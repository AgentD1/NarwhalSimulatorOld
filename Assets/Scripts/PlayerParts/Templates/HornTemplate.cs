using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HornTemplate : MonoBehaviour, IDamageValue {

    public float Damage { get; set; }

    public virtual void Start() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Particles = GetComponentInChildren<ParticleSystem>();
    }

}
