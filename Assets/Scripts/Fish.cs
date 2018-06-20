using UnityEngine;
using System.Collections.Generic;

public class Fish : MonoBehaviour {
    
    public GameObject player;
    public float health;
    public float scoreFromKill;
	public Color colorParticlesKill;
    public int particleAmount = 50;
    private Rigidbody2D rb;

    // states:
    // state 0 is idle
    // state 1 is run

    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider == null || col.collider.sharedMaterial == null || col.collider.sharedMaterial.name != "Horn") return;
        if (col.rigidbody.velocity.magnitude < 0) {
            health += col.rigidbody.velocity.magnitude;

        }
		health -= col.rigidbody.velocity.magnitude;
		//player.GetComponent<Player>().particles.startColor = colorParticlesKill;
		//player.GetComponent<Player> ().particles.Emit (Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
		player.GetComponent<Player>().ParticleEmit(colorParticlesKill, Mathf.RoundToInt (col.rigidbody.velocity.magnitude * particleAmount));
        if (health <= 0) {
            Destroy(gameObject);
            col.gameObject.GetComponent<Player>().score += scoreFromKill;
			//player.GetComponent<Player> ().particles.Emit (Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
			player.GetComponent<Player>().ParticleEmit(colorParticlesKill, Mathf.RoundToInt (col.rigidbody.velocity.magnitude * particleAmount));
		}

    }
}