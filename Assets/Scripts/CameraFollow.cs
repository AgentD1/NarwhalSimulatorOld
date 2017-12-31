using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject objectToFollow;
    public bool useX,useY,useZ;
    
	void Update () {
        if (useX) {
            transform.position = new Vector3(objectToFollow.transform.position.x, transform.position.y, transform.position.z);
        }
        if (useY) {
            transform.position = new Vector3(transform.position.x, objectToFollow.transform.position.y, transform.position.z);
        }
        if (useZ) {
            transform.position = new Vector3(transform.position.x, transform.position.y, objectToFollow.transform.position.z);
        }
    }
}
