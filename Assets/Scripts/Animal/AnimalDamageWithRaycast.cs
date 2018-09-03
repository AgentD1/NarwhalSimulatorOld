using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDamageWithRaycast : MonoBehaviour {
    
    public DamageTarget damageTarget;

    public float damage;

    public float range;

    public Transform directionObject;
    

	void FixedUpdate () {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range);

        if (hit != null) {
            if (damageTarget == DamageTarget.Animal || damageTarget == DamageTarget.All) {
                if (hit.GetComponent<IAnimalHealth>() != null) {
                    hit.GetComponent<IAnimalHealth>().Health -= damage;
                    if (hit.GetComponent<Animal>()) {
                        hit.GetComponent<Animal>().DealDamage(damage, GetComponent<ParticleSystem>());
                    } else {
                        Debug.Log("Oh no");
                    }
                } else {
                    if (hit.transform.parent.GetComponent<IAnimalHealth>() != null) {
                        hit.transform.parent.GetComponent<IAnimalHealth>().Health -= damage;
                        if (hit.GetComponent<Animal>()) {
                            hit.GetComponent<Animal>().DealDamage(damage, GetComponent<ParticleSystem>());
                        } else {
                            Debug.Log("Oh no");
                        }
                    }
                }
            }
            if (damageTarget == DamageTarget.Block || damageTarget == DamageTarget.All) {
                if (hit.GetComponent<IBlockHealth>() != null) {
                    hit.GetComponent<IBlockHealth>().Health -= damage;
                }
            }
        }
	}
}

public enum DamageTarget {
    Animal, Block, All 
}
