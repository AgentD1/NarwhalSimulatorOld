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
            Destroy(transform.GetChild(0).gameObject);
            Destroy(gameObject);
            col.gameObject.GetComponent<Player>().score += originalHealth * scoreMultiplier;
        } else {
            if (transform.GetChild(0).GetComponent<SpriteRenderer>() == null) {
                transform.GetChild(0).gameObject.AddComponent<SpriteRenderer>();
                
            }
        }
        for (int i = 0; i < breakTextures.GetLength(0); i++) {
            if (1 - (health / originalHealth) <= ((originalHealth / breakTextures.GetLength(0)) / originalHealth) * i) {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = breakTextures[i];
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