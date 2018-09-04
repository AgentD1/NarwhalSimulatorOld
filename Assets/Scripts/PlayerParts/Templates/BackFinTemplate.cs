using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BackFinTemplate : MonoBehaviour, IMovementSpeedModifiable, IMovementAccelerationModifiable {

    // Override these in override void Start() or from the inspector
    public float MovementSpeed { get; set; }
    public float MovementAcceleration { get; set; }

    public Rigidbody2D rb;

    public virtual void Start() {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

	public virtual void FixedUpdate () {
        if (Input.GetKey(KeyCode.W)) {
            //if (rb.velocity.x * rb.velocity.y < MovementSpeed) {
            //    rb.AddRelativeForce(new Vector2(-MovementAcceleration, 0), 0);
            //}

            rb.AddRelativeForce(new Vector2(-MovementAcceleration, 0), 0);
            
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MovementSpeed);

            //rb.AddRelativeForce(rb.velocity.normalized * Mathf.Clamp(currentSpeed + MovementAcceleration, rb.velocity.magnitude + MovementSpeed, MovementSpeed));
            //rb.velocity = rb.velocity.normalized * currentSpeed;
        }
        if (Input.GetKey(KeyCode.S)) {
            //if (rb.velocity.x * rb.velocity.y < MovementSpeed) {
            //    rb.AddRelativeForce(new Vector2(MovementAcceleration / 2, 0));
            //}
            float currentSpeed = rb.velocity.magnitude;
            currentSpeed = Mathf.Clamp(currentSpeed + MovementAcceleration, 0, MovementSpeed);
            rb.velocity = rb.velocity.normalized * currentSpeed;
        }

        transform.Find("Default").GetComponent<Animator>().SetFloat("SwimSpeed", rb.velocity.magnitude);
    }
}
