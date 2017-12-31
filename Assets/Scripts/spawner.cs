using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    
    public GameObject thingToSpawn;
    public int spawnInterval;
    private float spawncounter;

	// Use this for initialization
	void Start () {
        if (spawnInterval == 0) {
            enabled = false;
        }
        spawncounter = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawncounter <= Time.time) {
            spawncounter = Time.time + spawnInterval;
			if (GameObject.FindGameObjectsWithTag ("Fish").Length < 20) {
				Instantiate (thingToSpawn, transform.position, Quaternion.identity);
			}
        }
	}
}