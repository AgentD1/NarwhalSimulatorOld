using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatFish : MonoBehaviour {

    public Sprite scared;
    public Sprite asleep;
    public Sprite awake;
    SpriteRenderer sr;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void FixedUpdate () {
        if(GetComponent<AnimalSleepUntilDisturbed>().isSleeping) {
            sr.sprite = asleep; 
        } else if (GetComponent<AnimalRunFromPlayer>().isScared) {
            sr.sprite = scared;
        } else {
            sr.sprite = awake;
        }
    }
}
