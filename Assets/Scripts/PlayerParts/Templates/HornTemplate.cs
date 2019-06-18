using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HornTemplate : MonoBehaviour, IDamageValue {

    public float Damage { get; set; }
    public float damage;

    public virtual void Start() {
        Damage = damage;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Particles = GetComponentInChildren<ParticleSystem>();
    }

}
