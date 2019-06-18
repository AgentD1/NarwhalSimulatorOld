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
                    }
                } else {
                    if (hit.transform.parent.GetComponent<IAnimalHealth>() != null) {
                        hit.transform.parent.GetComponent<IAnimalHealth>().Health -= damage;
                        if (hit.GetComponent<Animal>()) {
                            hit.GetComponent<Animal>().DealDamage(damage, GetComponent<ParticleSystem>());
                        }
                    }
                }
            }
            if (damageTarget == DamageTarget.Block || damageTarget == DamageTarget.All) {
                if (hit.GetComponent<IBlockHealth>() != null) {
                    hit.GetComponent<IBlockHealth>().Health -= damage;
                }
            }
            if (hit.transform.parent != null && hit.transform.parent.tag == "Player" && hit.transform.parent.GetComponent<Player>().bodyParts["Horn"] != hit.gameObject) {
                ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams {
                    startColor = new Color(1f, 0f, 0f),
                    position = transform.position,
                    applyShapeToPosition = true
                };
                GetComponent<ParticleSystem>().Emit(emitParams, Mathf.RoundToInt(damage * 20f));
                //hit.transform.parent.GetComponent<Player>().ParticleEmit(new Color(1f, 0f, 0f), Mathf.RoundToInt(damage * 20f));
                hit.transform.parent.GetComponent<Player>().Health -= damage;
            }
        }
	}
}

public enum DamageTarget {
    Animal, Block, All 
}
