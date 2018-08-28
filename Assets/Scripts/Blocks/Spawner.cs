using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    
    public GameObject thingToSpawn;
    public int spawnInterval;
    private float spawncounter;

    public int limit;

    List<GameObject> spawnedGameObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
        if (spawnInterval == 0) {
            Debug.LogWarning("Spawn interval 0! Stopping...");
            enabled = false;
        }

        if(limit == 0) {
            Debug.LogWarning("Limit 0! Stopping...");
            enabled = false;
        }

        spawncounter = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawncounter <= Time.time) {
            spawncounter = Time.time + spawnInterval;
            if (spawnedGameObjects.Count < limit) {
                GameObject go = Instantiate(thingToSpawn, transform.position, Quaternion.identity);
                spawnedGameObjects.Add(go);
            } else {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject longest = spawnedGameObjects[0];
                float longestDistance = 0;
                foreach (GameObject go in spawnedGameObjects) {
                    if (longest == null) {
                        longest = go;
                        longestDistance = Vector2.Distance(go.transform.position, player.transform.position);
                    } else {
                        if (Vector2.Distance(go.transform.position, player.transform.position) > longestDistance) {
                            longest = go;
                            longestDistance = Vector2.Distance(go.transform.position, player.transform.position);
                        }
                    }
                }

                spawnedGameObjects.Remove(longest);
                Destroy(longest);

                GameObject goo = Instantiate(thingToSpawn, transform.position, Quaternion.identity);
                spawnedGameObjects.Add(goo);
            }
        }
	}
}