using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopFinDefault : MonoBehaviour {

    public Component backFin;
    public IMovementSpeedModifiable moveBackFin;
    public IMovementAccelerationModifiable accelerationBackFin;

    public float chargeTime;
    public float charge;
    public float movementSpeedMultiplier;
    public float movementAccelerationMultiplier;
    public float chargeRegen;
    public float chargeUseRate;
    float originalSpeed;
    float originalAcceleration;
    bool isCharging = false;
    public Slider chargeSlider;

    bool isInitialized = false;


    public void Start() {
        // Initialize after BackFin, just in case
        StartCoroutine(LateStart(0.1f));
    }

    public void FixedUpdate() {
        if (!isInitialized) {
            return;
        }

        if (isCharging) {
            Debug.Log("Charging... " + Time.timeScale);
            if (charge > 0 && Input.GetKey(KeyCode.LeftShift)) {                                        // if charging
                charge = Mathf.Clamp(charge - (Time.fixedDeltaTime * chargeUseRate), 0, chargeTime);
                chargeSlider.value = charge / chargeTime;
            } else {                                                                                    // if is not charging anymore
                moveBackFin.MovementSpeed = originalSpeed;
                if (accelerationBackFin != null) {
                    accelerationBackFin.MovementAcceleration = originalAcceleration;
                }
                isCharging = false;
            }
        } else {
            if (charge > chargeRegen && Input.GetKey(KeyCode.LeftShift)) {                              // if not charging but wants to charge
                moveBackFin.MovementSpeed = originalSpeed * movementSpeedMultiplier;
                if (accelerationBackFin != null) {
                    accelerationBackFin.MovementAcceleration = originalAcceleration * movementAccelerationMultiplier;
                }
                isCharging = true;
            } else {                                                                                    // if not charging
                charge = Mathf.Clamp(charge + (Time.fixedDeltaTime * chargeRegen), 0, chargeTime);
                chargeSlider.value = charge / chargeTime;
            }
        }
    }

    IEnumerator LateStart(float n) {
        yield return new WaitForSeconds(n);


        backFin = transform.parent.Find("BackFin").GetComponent("IMovementSpeedModifiable");

        if (backFin is IMovementAccelerationModifiable) {
            accelerationBackFin = backFin as IMovementAccelerationModifiable;
        } else {
            Debug.LogWarning("TopFin can't convert backFin to a IMovementAccelerationModifiable!");
        }

        moveBackFin = backFin as IMovementSpeedModifiable;
        // No need for another because it just won't find anything if there is no IMovementSpeedModifiable.

        if (backFin == null) {
            Debug.LogWarning("TopFinDefault can't find a boostable IMovementSpeedModifiable and is now dead");
            Destroy(this, 0.001f);
        } else {
            originalSpeed = moveBackFin.MovementSpeed;
            if (accelerationBackFin != null) {
                originalAcceleration = accelerationBackFin.MovementAcceleration;
            }

        }

        isInitialized = true;
    }

}
