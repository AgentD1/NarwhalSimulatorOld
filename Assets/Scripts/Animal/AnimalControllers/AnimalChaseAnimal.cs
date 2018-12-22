using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalChaseAnimal : MonoBehaviour {

    public float distanceToAgro;
    public float distanceToUnAgro;

    public bool isAgressive;

    public string[] animalTypesAgressiveTo;

    public void CalculateWeightedDirection() {
        if (isAgressive) {
            /*
            if (Vector2.Distance(transform.position, player.transform.position) < distanceToUnscare) {
                GetComponent<Animal>().desiredDirections.Add(new WeightedDirection(transform.position - player.transform.position, 1f, WeightedDirectionType.OVERRIDE));
            } else {
                isScared = false;
            }
            */
            List<Animal> animalsScaredOf = new List<Animal>();

            foreach (string type in animalTypesAgressiveTo) {
                if (Animal.animalsByType.ContainsKey(type)) {
                    animalsScaredOf.AddRange(Animal.animalsByType[type]);
                }
            }

            float closestDistance = distanceToUnAgro;
            WeightedDirection wd = new WeightedDirection(Vector2.zero, 0, WeightedDirectionType.DEFAULT);

            foreach (Animal a in animalsScaredOf) {
                if (Vector2.Distance(a.transform.position, transform.position) < closestDistance) {
                    isAgressive = true;
                    closestDistance = Vector2.Distance(a.transform.position, transform.position);
                    wd = new WeightedDirection(a.transform.position - transform.position, 5f, WeightedDirectionType.DEFAULT);
                }
            }

            if (wd.weight != 0) {
                GetComponent<Animal>().desiredDirections.Add(wd);
            }
        } else {
            /*
            if (Vector2.Distance(transform.position, player.transform.position) < distanceToScare) {
                GetComponent<Animal>().desiredDirections.Add(new WeightedDirection(transform.position - player.transform.position, 1f, WeightedDirectionType.OVERRIDE));
                isScared = true;
            }
            */

            List<Animal> animalsScaredOf = new List<Animal>();

            foreach (string type in animalTypesAgressiveTo) {
                if (Animal.animalsByType.ContainsKey(type)) {
                    animalsScaredOf.AddRange(Animal.animalsByType[type]);
                }
            }

            float closestDistance = distanceToUnAgro;
            WeightedDirection wd = new WeightedDirection(Vector2.zero, 0, WeightedDirectionType.DEFAULT);

            foreach (Animal a in animalsScaredOf) {
                if (Vector2.Distance(a.transform.position, transform.position) < closestDistance) {
                    isAgressive = true;
                    closestDistance = Vector2.Distance(a.transform.position, transform.position);
                    wd = new WeightedDirection(a.transform.position - transform.position, 5f, WeightedDirectionType.DEFAULT);
                }
            }

            if (wd.weight != 0) {
                GetComponent<Animal>().desiredDirections.Add(wd);
            }
        }
    }
}
