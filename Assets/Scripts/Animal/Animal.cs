using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IAnimalHealth {

    Rigidbody2D rb;
    
    public float Health { get; set; }

    static public Dictionary<string, List<Animal>> animalsByType;

    public string animalType;

    public bool turn;

    public float turnSpeed;
    public float moveSpeed;

    public bool rotateInDirection;

    public List<WeightedDirection> desiredDirections;

    void Start () {
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


    private void OnDestroy() {
        animalsByType[animalType].Remove(this);
    }

    void Update () {
		
	}
}
