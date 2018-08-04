using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SideFinTemplate : MonoBehaviour {

    // Set turnspeed in override Start() or in the inspector

    public float turnSpeed;
    public Rigidbody2D rb;

	public virtual void Start () {
        rb = transform.parent.GetComponent<Rigidbody2D>();
	}
	
	public virtual void FixedUpdate () {
        if (Input.GetKey(KeyCode.A)) {
            rb.AddTorque(turnSpeed);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddTorque(-turnSpeed);
        }
    }
}
