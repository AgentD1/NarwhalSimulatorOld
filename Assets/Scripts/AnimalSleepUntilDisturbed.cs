using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSleepUntilDisturbed : MonoBehaviour {

    public float chanceToStartSleeping = 500f;

    public bool wakeUpAfterTime = true;
    public float sleepTimeMin = 10f;
    public float sleepTimeMax = 120f;

    public bool wakeUpAfterDisturbed = true;
    public bool wakeUpAfterDisturbedByPlayer = true;
    public string[] animalsToBeDisturbedBy = new string[] { "Shark" };
    public float distanceToBeDisturbed = 2f;
    public float minimunSpeedToDisturb = 5f;

    public bool isSleeping = false;

    GameObject player;

    public GameObject zzzParticles;

    public void Start() {
        player = GameObject.FindWithTag("Player");
        zzzParticles = transform.GetChild(0).gameObject;
        zzzParticles.GetComponent<ParticleSystem>().Stop();
    }

    public void CalculateWeightedDirection() {
        if(Mathf.CeilToInt(Random.Range(0f, chanceToStartSleeping)) == chanceToStartSleeping && Vector2.Distance(player.transform.position, transform.position) >= distanceToBeDisturbed) {
            Sleep();
        }
    }

    public void OnCollisionEnter2D() {
        if (!isSleeping) {
            return;
        }
        Wake();
    }

    public void FixedUpdate() {
        if (isSleeping) {
            if (wakeUpAfterDisturbed) {
                foreach (string animalType in animalsToBeDisturbedBy) {
                    List<Animal> animals;
                    if (Animal.animalsByType.TryGetValue(animalType, out animals)) {
                        foreach (Animal animal in animals) {
                            if (Vector2.Distance(animal.transform.position, transform.position) <= distanceToBeDisturbed && animal.GetComponent<Rigidbody2D>().velocity.magnitude > minimunSpeedToDisturb) {
                                Wake();
                                return;
                            }
                        }
                    }
                }
            }
            if(wakeUpAfterDisturbedByPlayer) {
                if (Vector2.Distance(player.transform.position, transform.position) <= distanceToBeDisturbed && player.GetComponent<Rigidbody2D>().velocity.magnitude > minimunSpeedToDisturb) {
                    Wake();
                    return;
                }

            }
        }
    }

    IEnumerator SleepForLengthOfTime(float time) {
        yield return new WaitForSeconds(time);
        Wake();
    }

    public void Sleep() {
        isSleeping = true;
        GetComponent<Animal>().enabled = false;
        GetComponent<Rigidbody2D>().drag = 1000;
        GetComponent<Rigidbody2D>().angularDrag = 1000;
        StartCoroutine(SleepForLengthOfTime(Random.Range(sleepTimeMin, sleepTimeMax)));
        zzzParticles.SetActive(true);
        zzzParticles.GetComponent<ParticleSystem>().Play();
    }

    public void Wake() {
        isSleeping = false;
        StopCoroutine("SleepForLengthOfTime");
        GetComponent<Animal>().enabled = true;
        GetComponent<Rigidbody2D>().drag = 0;
        zzzParticles.SetActive(false);
        GetComponent<Rigidbody2D>().angularDrag = 0.05f;
        zzzParticles.GetComponent<ParticleSystem>().Stop();
        zzzParticles.GetComponent<ParticleSystem>().Clear();
    }
}