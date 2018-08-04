using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRunFromPlayer : MonoBehaviour {

    Transform player;

    public float distanceToScare;
    public float distanceToUnscare;

    public bool isScared = false;

    void Start() {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void CalculateWeightedDirection() {
        if (isScared) {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceToUnscare) {
                GetComponent<Animal>().desiredDirections.Add(new WeightedDirection(transform.position - player.transform.position, 1f, WeightedDirectionType.OVERRIDE));
            } else {
                isScared = false;
            }
        } else {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceToScare) {
                GetComponent<Animal>().desiredDirections.Add(new WeightedDirection(transform.position - player.transform.position, 1f, WeightedDirectionType.OVERRIDE));
                isScared = true;
            }
        }
    }
}
