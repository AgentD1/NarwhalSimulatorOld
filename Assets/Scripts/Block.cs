using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    public float health;
    float originalHealth;
    public float scoreMultiplier = 1f;
    public Color playerParticleColor;
    public Sprite[] breakTextures;
    void Start() {
        if (health <= 0) {
            Debug.LogError("Block health cannot be less than or equal to 0 on start! Setting to 1. ID: " + GetInstanceID());
            health = 1;
        }
        originalHealth = health;
    }
    public void OnCollisionEnter2D(Collision2D col) {
        if (col.collider == null || col.collider.sharedMaterial == null || col.collider.sharedMaterial.name != "Horn") return;
        if (col.rigidbody.velocity.magnitude < 0) {
            health += col.rigidbody.velocity.magnitude;
        } else {
            health -= col.rigidbody.velocity.magnitude;
        }
        if (health <= 0) {
            Destroy(gameObject);
            col.gameObject.GetComponent<Player>().score += originalHealth * scoreMultiplier;
        } else {
            if(transform.childCount == 0 || transform.Find("CracksDisplay") == null) {
                GameObject go = new GameObject();
                go.transform.parent = transform;
                go.transform.position = transform.position - Vector3.forward;
                go.transform.rotation = transform.rotation;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.name = "CracksDisplay";
            }
            if (transform.Find("CracksDisplay").GetComponent<SpriteRenderer>() == null) {
                transform.Find("CracksDisplay").gameObject.AddComponent<SpriteRenderer>();
                
            }
        }
        for (int i = 0; i < breakTextures.GetLength(0); i++) {
            if (1 - (health / originalHealth) <= ((originalHealth / breakTextures.GetLength(0)) / originalHealth) * i) {
                transform.Find("CracksDisplay").GetComponent<SpriteRenderer>().sprite = breakTextures[i];
                break;
            }
        }

        /*col.gameObject.GetComponent<Player>().particles.transform.position = col.contacts[0].point;
        col.gameObject.GetComponent<Player>().particles.startColor = playerParticleColor;
        col.gameObject.GetComponent<Player>().particles.Emit(Mathf.RoundToInt(col.rigidbody.velocity.magnitude*50));
        */
		col.gameObject.GetComponent<Player>().ParticleEmit(playerParticleColor, Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
    }
}