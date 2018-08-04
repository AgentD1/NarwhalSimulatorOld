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
            if (rb.velocity.x * rb.velocity.y < MovementSpeed) {
                rb.AddRelativeForce(new Vector2(-MovementAcceleration, 0), 0);
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            if (rb.velocity.x * rb.velocity.y < MovementSpeed) {
                rb.AddRelativeForce(new Vector2(MovementAcceleration / 2, 0));
            }
        }

        transform.Find("Default").GetComponent<Animator>().SetFloat("SwimSpeed", rb.velocity.magnitude);
    }
}
