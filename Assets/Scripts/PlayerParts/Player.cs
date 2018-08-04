using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    /*
    public float movementAcceleration;
    Rigidbody2D myRigidBody;
    public float movementSpeed;
    public float rotateSpeed;

    public float chargeTime;
    public float charge;
    public float movementSpeedMultiplier;
    public float movementAccelerationMultiplier;
    public float chargeRegen;
    public float chargeUseRate;
    float originalSpeed;
    float originalAcceleration;
    bool isCharging = false;
    public Slider chargeSlider;
    */

    public float score;
    public Text text;
    public ParticleSystem particles;


    // Use this for initialization
    void Start () {
        //myRigidBody = GetComponent<Rigidbody2D>();
        particles = GameObject.Find("PlayerParticleEmitter").GetComponent<ParticleSystem>();
        score = 0;
        //originalSpeed = movementSpeed;
        //originalAcceleration = movementAcceleration;
	}

    // Update is called once per frame
    void FixedUpdate() {        
        text.text = score.ToString();
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.R)) {
            transform.position = Vector2.zero;
        }
        /*
        if (isCharging) {
            if (charge > 0 && Input.GetKey(KeyCode.LeftShift)) {                                        // if charging
                charge = Mathf.Clamp(charge - (Time.fixedDeltaTime * chargeUseRate), 0, chargeTime);
                chargeSlider.value = charge / chargeTime;
            } else {                                                                                    // if is not charging anymore
                movementSpeed = originalSpeed;
                movementAcceleration = originalAcceleration;
                isCharging = false;
            }
        } else {
            if (charge > chargeRegen && Input.GetKey(KeyCode.LeftShift)) {                              // if not charging but wants to charge
                movementSpeed = originalSpeed * movementSpeedMultiplier;
                movementAcceleration = originalAcceleration * movementAccelerationMultiplier;
                isCharging = true;
            } else {                                                                                    // if not charging
                charge = Mathf.Clamp(charge + (Time.fixedDeltaTime * chargeRegen), 0, chargeTime);
                chargeSlider.value = charge / chargeTime;   
            }
        }
        
        */
    }

	public void ParticleEmit(Color color, int amountToEmit){
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams {
            startColor = color,
            position = particles.transform.position,
            applyShapeToPosition = true
        };
        particles.Emit(emitParams, amountToEmit);
	}
}