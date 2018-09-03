using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRunFromAnimal : MonoBehaviour {

    public float distanceToScare;
    public float distanceToUnscare;

    public bool isScared;

    public string[] animalTypesScaredOf;

    public void CalculateWeightedDirection() {
        if (isScared) {
            /*
            if (Vector2.Distance(transform.position, player.transform.position) < distanceToUnscare) {
                GetComponent<Animal>().desiredDirections.Add(new WeightedDirection(transform.position - player.transform.position, 1f, WeightedDirectionType.OVERRIDE));
            } else {
                isScared = false;
            }
            */
            List<Animal> animalsScaredOf = new List<Animal>();
            
            foreach (string type in animalTypesScaredOf) {
                animalsScaredOf.AddRange(Animal.animalsByType[type]);
            }

            List<WeightedDirection> wds = new List<WeightedDirection>();

            foreach(Animal a in animalsScaredOf) {
                if(Vector2.Distance(a.transform.position, transform.position) > distanceToUnscare) {
                    isScared = false;
                } else {
                    wds.Add(new WeightedDirection(transform.position - a.transform.position, 5f, WeightedDirectionType.DEFAULT));
                }
            }
            GetComponent<Animal>().desiredDirections.AddRange(wds);
        } else {
            /*
            if (Vector2.Distance(transform.position, player.transform.position) < distanceToScare) {
                GetComponent<Animal>().desiredDirections.Add(new WeightedDirection(transform.position - player.transform.position, 1f, WeightedDirectionType.OVERRIDE));
                isScared = true;
            }
            */

            List<Animal> animalsScaredOf = new List<Animal>();

            foreach (string type in animalTypesScaredOf) {
                animalsScaredOf.AddRange(Animal.animalsByType[type]);
            }

            List<WeightedDirection> wds = new List<WeightedDirection>();

            foreach (Animal a in animalsScaredOf) {
                if (Vector2.Distance(a.transform.position, transform.position) < distanceToUnscare) {
                    isScared = true;
                    wds.Add(new WeightedDirection(transform.position - a.transform.position, 5f, WeightedDirectionType.DEFAULT));
                }
            }
            GetComponent<Animal>().desiredDirections.AddRange(wds);
        }
    }
}
