using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IAnimalHealth {

    public float Health {
        get {
            return health;
        }
        set {
            if(value < health) {
                secondsSinceDamageLastTaken = 0f;
            }
            health = value;
        }
    }

    [HideInInspector]
    public float maxHealth;

    float health = 10f;

    public float secondsUntilHealthRegenAfterDamage = 5f;

    public float regenPerSecond = 1f;

    float secondsSinceDamageLastTaken = 0f;

    public Dictionary<string, GameObject> bodyParts;

    public float score;
    public Text text;
    public ParticleSystem Particles;
    


    // Use this for initialization
    void Start () {
        maxHealth = health;

        //myRigidBody = GetComponent<Rigidbody2D>();

        score = 0;
        bodyParts = new Dictionary<string, GameObject>();

        ShopController sc = GameObject.FindObjectOfType<ShopController>();

        sc.GiveItem("Default Horn");
        sc.GiveItem("Default Back Fin");
        sc.GiveItem("Default Top Fin");
        sc.GiveItem("Default Side Fins");
        sc.GiveItem("Default Body");

        sc.EquipItemToPlayer("Default Horn");
        sc.EquipItemToPlayer("Default Back Fin");
        sc.EquipItemToPlayer("Default Top Fin");
        sc.EquipItemToPlayer("Default Side Fins");
        sc.EquipItemToPlayer("Default Body");

        Debug.Log(bodyParts["Horn"].name);
    }

    // Update is called once per frame
    void FixedUpdate() {
        secondsSinceDamageLastTaken += Time.fixedDeltaTime;

        if(secondsSinceDamageLastTaken > secondsUntilHealthRegenAfterDamage) {
            health = Mathf.Clamp(health + (regenPerSecond * Time.fixedDeltaTime), 0, maxHealth);
        }

        text.text = score.ToString();
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.R)) {
            transform.position = Vector2.zero;
        }
    }

	public void ParticleEmit(Color color, int amountToEmit){
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams {
            startColor = color,
            position = Particles.transform.position,
            applyShapeToPosition = true
        };
        Particles.Emit(emitParams, amountToEmit);
    }
}