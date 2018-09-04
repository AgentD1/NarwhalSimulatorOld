using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IAnimalHealth {

    Rigidbody2D rb;
    
    public float Health {
        get {
            return healthForInspector;
        }
        set {
            healthForInspector = value;
        }
    }

    public float healthForInspector;

    static public Dictionary<string, List<Animal>> animalsByType;

    public string animalType;

    public bool turn;

    public float turnSpeed;
    public float moveSpeed;

    public bool rotateInDirection;

    public List<WeightedDirection> desiredDirections;


    public GameObject player;
    public float scoreFromKill;
    public Color colorParticlesKill;
    public int particleAmount = 50;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
		if(animalsByType == null) {
            animalsByType = new Dictionary<string, List<Animal>>();
        }
        if (!animalsByType.ContainsKey(animalType)) {
            animalsByType.Add(animalType, new List<Animal>());
        }
        animalsByType[animalType].Add(this);


    }

    void FixedUpdate() {
        if(Health <= 0) {
            Destroy(gameObject);
        }
        desiredDirections = new List<WeightedDirection>();

        BroadcastMessage("CalculateWeightedDirection", SendMessageOptions.DontRequireReceiver);

        Vector2 dirFallback = Vector2.zero;
        Vector2 dirOverride = Vector2.zero;
        Vector2 dir = Vector2.zero;
        Vector2 endDir = Vector2.zero;

        foreach (WeightedDirection wd in desiredDirections) {
            if(wd.type == WeightedDirectionType.OVERRIDE) {
                dirOverride += wd.direction * wd.weight;
            } else if(wd.type == WeightedDirectionType.FALLBACK) {
                dirFallback += wd.direction * wd.weight;
            } else {
                dir += wd.direction * wd.weight;
            }
        }

        if(dirOverride == Vector2.zero) {
            if(dir == Vector2.zero) {
                endDir = dirFallback;
            } else {
                endDir = dir;
            }
        } else {
            endDir = dirOverride;
        }
        float currentAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        float wantedAngle = Mathf.Atan2(endDir.y, endDir.x) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(currentAngle, wantedAngle, turnSpeed);
        
        rb.velocity = JMath.DegreeToVector2(angle) * moveSpeed;

        transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider == null || col.collider.sharedMaterial == null || col.collider.sharedMaterial.name != "Horn") return;
        if (col.rigidbody.velocity.magnitude < 0) {
            //Health += col.rigidbody.velocity.magnitude;
            DealDamage(-col.rigidbody.velocity.magnitude, player.GetComponent<Player>().Particles);
        } else {
            //    Health -= col.rigidbody.velocity.magnitude;
            DealDamage(col.rigidbody.velocity.magnitude, player.GetComponent<Player>().Particles);
        }
        //player.GetComponent<Player>().particles.startColor = colorParticlesKill;
        //player.GetComponent<Player> ().particles.Emit (Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
        //player.GetComponent<Player>().ParticleEmit(colorParticlesKill, Mathf.RoundToInt(col.rigidbody.velocity.magnitude * particleAmount));
        if (Health <= 0) {
            Destroy(gameObject);
            col.gameObject.GetComponent<Player>().score += scoreFromKill;
            //player.GetComponent<Player> ().particles.Emit (Mathf.RoundToInt (col.rigidbody.velocity.magnitude * 50));
            //player.GetComponent<Player>().ParticleEmit(colorParticlesKill, Mathf.RoundToInt(col.rigidbody.velocity.magnitude * particleAmount));
        }

    }

    public void DealDamage(float damage, ParticleSystem particles) {
        Health -= damage;
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams {
            startColor = colorParticlesKill,
            position = particles.transform.position,
            applyShapeToPosition = true
        };
        particles.Emit(emitParams, Mathf.RoundToInt(damage * particleAmount));
    }


    private void OnDestroy() {
        animalsByType[animalType].Remove(this);
    }

    void Update () {
		
	}
}
