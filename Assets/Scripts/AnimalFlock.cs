using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFlock : MonoBehaviour {

    public float seperationDistance;
    public float seperationForce;
    public float cohesionDistance;
    public float cohesionForce;
    public float alignmentForce;

    Animal[] fishNearby;

    public void CalculateWeightedDirection() {
        Animal[] fishNearbyTemp = Animal.animalsByType[GetComponent<Animal>().animalType].ToArray();
        List<Animal> fishNearbyList = new List<Animal>();

        foreach(Animal fish in fishNearbyTemp) {
            if(Vector2.Distance(transform.position, fish.transform.position) < cohesionDistance) {
                fishNearbyList.Add(fish);
            }
        }

        fishNearby = fishNearbyList.ToArray();
        fishNearbyList = null;
        fishNearbyTemp = null;

        Cohesion();
        Seperation();
        Alignment();
        
        fishNearby = null;
    }

    void Cohesion() {
        foreach(Animal fish in fishNearby) {
            WeightedDirection wd = new WeightedDirection(fish.transform.position - transform.position, cohesionForce / fishNearby.Length, WeightedDirectionType.DEFAULT);
            GetComponent<Animal>().desiredDirections.Add(wd);
        }
    }

    void Seperation() {
        foreach (Animal fish in fishNearby) {
            if (Vector2.Distance(transform.position, fish.transform.position) < seperationDistance) {
                WeightedDirection wd = new WeightedDirection(-(fish.transform.position - transform.position), seperationForce, WeightedDirectionType.DEFAULT);
                GetComponent<Animal>().desiredDirections.Add(wd);
            }
        }
    }

    void Alignment() {
        float[] rotations = new float[fishNearby.Length];
        for (int i = 0; i < fishNearby.Length; i++) {
            rotations[i] = fishNearby[i].transform.rotation.eulerAngles.z;
        }

        float finalRotation = JMath.Average(rotations);

        WeightedDirection wd = new WeightedDirection(JMath.DegreeToVector2(finalRotation), alignmentForce, WeightedDirectionType.DEFAULT);
        GetComponent<Animal>().desiredDirections.Add(wd);
    }
}
