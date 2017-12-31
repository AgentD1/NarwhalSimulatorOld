using UnityEngine;
using System.Collections;

public class rainbowFish : MonoBehaviour {

    private SpriteRenderer spr;
    private Color col;
    [SerializeField()]
    private float rainbowRotationRate = 0.1f;

    // Use this for initialization
    void Start () {
        spr = GetComponent<SpriteRenderer>();
        col = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
        float h;
        float s;
        float v;
        Color.RGBToHSV(col, out h, out s, out v);
        col = Color.HSVToRGB(h + rainbowRotationRate, 1, 1);
        spr.color = col;
	}
}
