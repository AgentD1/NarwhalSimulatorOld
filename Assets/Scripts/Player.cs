using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float movementSpeed;
    Rigidbody2D myRigidBody;
    public float maxMoveSpeed;
    public float rotateSpeed;
    public float score;
    public Text text;
    public ParticleSystem particles;
	public Gradient alphaGradient;
	public Gradient result;

    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        score = 0;
        
        
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetKey(KeyCode.W)) {
            if (myRigidBody.velocity.x * myRigidBody.velocity.y < maxMoveSpeed) {
                myRigidBody.AddRelativeForce(new Vector2(-movementSpeed, 0), 0);
            }
        }
        text.text = score.ToString();
        if (Input.GetKey(KeyCode.S)) {
            if (myRigidBody.velocity.x * myRigidBody.velocity.y < maxMoveSpeed) {
                myRigidBody.AddRelativeForce(new Vector2(movementSpeed / 2, 0));
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