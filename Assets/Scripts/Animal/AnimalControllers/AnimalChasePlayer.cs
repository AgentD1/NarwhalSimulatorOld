using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalChasePlayer : MonoBehaviour {

    Transform player;

    public float distanceToAgro;
    public float distanceToUnagro;

    public bool isAgressive = false;

    void Start() {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void CalculateWeightedDirection() {
        if (isAgressive) {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceToUnagro) {
                GetComponent<Animal>().desiredDirections.Add(new WeightedDirection(player.transform.position - transform.position, 1f, WeightedDirectionType.OVERRIDE));
            } else {
                isAgressive = false;
            }
        } else {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceToAgro) {
                GetComponent<Animal>().desiredDirections.Add(new WeightedDirection(player.transform.position - transform.position, 1f, WeightedDirectionType.OVERRIDE));
                isAgressive = true;
            }
        }
    }
}
