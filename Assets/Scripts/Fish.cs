using UnityEngine;
using System.Collections.Generic;

public class Fish : MonoBehaviour {

    public byte state = 0;
    public GameObject player;
    [SerializeField()]
    private float distanceToBecomeScared;
    [SerializeField()]
    private float distanceToStopBeingScared;
    public float health;
    public float scoreFromKill;
	public Color colorParticlesKill;
    private float angleTarget;
    [SerializeField()]
    private float closeTo;
    private Rigidbody2D rb;
	public float neighborhoodSize;
	public float cohesionForce;
	public float moveSpeed;
	public float scaredMoveSpeed;
	public float seperationDist;
	public float seperationForce;
	public float rotationSpeed = 50f;
	public float randomRotation = 2;

    // states:
    // state 0 is idle
    // state 1 is run

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
		Debug.Log ("Done");
    }
    void FixedUpdate()
    {
		if (state == 1) {
			rb.velocity = (transform.position - player.transform.position).normalized * scaredMoveSpeed;
			rb.rotation = Vector2.Angle(Vector2.down, rb.velocity.normalized);
		} else {
			GameObject[] fishGameObjects = GameObject.FindGameObjectsWithTag ("Fish");
			List<Vector3> fishPositions = new List<Vector3> ();
			List<float> fishRotations = new List<float> ();
			for (int i = 0; i < fishGameObjects.Length; i++) {
				GameObject fish = fishGameObjects [i];
				if (fish.GetInstanceID () == GetInstanceID ()) {
					continue;
				}
				if (Vector3.Distance (fish.transform.position, transform.position) > neighborhoodSize) {
					continue;
				}
				if (Vector3.Distance (fish.transform.position, transform.position) < seperationDist) {
					rb.AddForce ((fish.transform.position - transform.position).normalized * -seperationForce);
				}

				fishRotations.Add (fish.transform.rotation.eulerAngles.z);
				fishPositions.Add (fish.transform.position);
			}



			Vector3 average = Jmath.Average (fishPositions.ToArray ());
			float averageRot = Jmath.Average (fishRotations.ToArray ());

			//rb.rotation = Vector2.Angle (Vector2.up, rb.velocity);

			if (Mathf.Approximately (rb.rotation, averageRot)) {
			} else if (rb.rotation < averageRot) {
				//rb.AddTorque (rotationSpeed);
				rb.rotation+=rotationSpeed;
			} else {
				//rb.AddTorque (-rotationSpeed);
				rb.rotation-=rotationSpeed;
			}
			rb.rotation += Random.Range (-randomRotation, randomRotation);

			rb.AddForce ((transform.position - average).normalized * -cohesionForce);

			rb.AddForce(-transform.right * moveSpeed);

			rb.velocity = rb.velocity.normalized * moveSpeed;
		}
		/*
		RaycastHit2D hit = Physics2D.Raycast (-transform.right * GetComponent<CircleCollider2D> ().radius, rb.velocity.normalized, 1, LayerMask.GetMask("FishCollidables"));
		if(hit != null){
			if (Vector2.Angle (rb.velocity, hit.normal) > 0) {
				rb.rotation += rotationSpeed;
			} else {
				rb.rotation -= rotationSpeed;
			}
		}
		*/
    }

	void Update(){
		if (Vector2.Distance (transform.position, player.transform.position) < distanceToBecomeScared && state == 0) {
			state = 1;
		} else if (Vector2.Distance (transform.position, player.transform.position) > distanceToStopBeingScared && state == 1) {
			state = 0;
		}
	}
    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider == null || col.collider.sharedMaterial == null || col.collider.sharedMaterial.name != "Horn") return;
        if (col.rigidbody.velocity.magnitude < 0) {
            health += col.rigidbody.velocity.magnitude;

        }
		health -= col.rigidbody.velocity.magnitude;
		//player.GetComponent<Player>().particles.startColor = colorParticlesKill;
		//player.GetComponent<Player> ().particles.Emit (Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
		player.GetComponent<Player>().ParticleEmit(colorParticlesKill, Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
        if (health <= 0) {
            Destroy(gameObject);
            col.gameObject.GetComponent<Player>().score += scoreFromKill;
			//player.GetComponent<Player> ().particles.Emit (Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
			player.GetComponent<Player>().ParticleEmit(colorParticlesKill, Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
		}

    }
    /*void OnBecameInvisible() {
        Destroy(gameObject);
    }*/
}