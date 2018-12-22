using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class WeightedDirection {

    public Vector2 direction;
    public float weight;
    public WeightedDirectionType type;

    public WeightedDirection(Vector2 direction, float weight, WeightedDirectionType type) {
        this.direction = direction;
        this.weight = weight;
        this.type = type;
    }

}

[System.Serializable()]
public enum WeightedDirectionType { DEFAULT, FALLBACK, OVERRIDE }
