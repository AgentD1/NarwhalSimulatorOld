using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float movementAcceleration;
    Rigidbody2D myRigidBody;
    public float movementSpeed;
    public float rotateSpeed;
    public float score;
    public Text text;
    public ParticleSystem particles;
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

    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        score = 0;
        originalSpeed = movementSpeed;
        originalAcceleration = movementAcceleration;
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetKey(KeyCode.W)) {
            if (myRigidBody.velocity.x * myRigidBody.velocity.y < movementSpeed) {
                myRigidBody.AddRelativeForce(new Vector2(-movementAcceleration, 0), 0);
            }
        }
        text.text = score.ToString();
        if (Input.GetKey(KeyCode.S)) {
            if (myRigidBody.velocity.x * myRigidBody.velocity.y < movementSpeed) {
                myRigidBody.AddRelativeForce(new Vector2(movementAcceleration / 2, 0));
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            myRigidBody.AddTorque(rotateSpeed);
        }
        if (Input.GetKey(KeyCode.D)) {
            myRigidBody.AddTorque(-rotateSpeed);
        }
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.R)) {
            transform.position = Vector2.zero;
        }

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

        GetComponent<Animator>().SetFloat("SwimSpeed", myRigidBody.velocity.magnitude);
    }

	public void ParticleEmit(Color color, int amountToEmit){
		/*GradientColorKey[] colorKeys = {
			new GradientColorKey (color, 0f),
			new GradientColorKey (color, 1f)
		};
		Gradient colorGradient = new Gradient ();
		colorGradient.SetKeys (colorKeys, alphaGradient.alphaKeys);
		result = colorGradient;
		ParticleSystem.MinMaxGradient grad = new ParticleSystem.MinMaxGradient (colorGradient);
		var colorOverLifetime = particles.colorOverLifetime;*/
		particles.startColor = color;
		//colorOverLifetime.color = grad;
		particles.Emit (amountToEmit);
	}
}