using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTurnRandomly : MonoBehaviour {

    public float turnSpeed;
    public bool goingLeft;
    public int chanceOfChangeDirection = 60;

    void Start() {
        goingLeft = Random.value > 0.5f;
    }

    public void CalculateWeightedDirection() {
        float currentAngle = transform.rotation.eulerAngles.z;
        WeightedDirection wd;

        if(Mathf.CeilToInt(Random.Range(0f,chanceOfChangeDirection)) == chanceOfChangeDirection) {
            goingLeft = !goingLeft;
        }

        if (goingLeft) {
            wd = new WeightedDirection(JMath.DegreeToVector2(currentAngle - turnSpeed), 1f, WeightedDirectionType.DEFAULT);
        } else {
            wd = new WeightedDirection(JMath.DegreeToVector2(currentAngle + turnSpeed), 1f, WeightedDirectionType.DEFAULT);
        }
        GetComponent<Animal>().desiredDirections.Add(wd);
    }
}