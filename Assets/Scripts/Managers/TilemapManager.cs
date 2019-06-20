using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour {
    public static Dictionary<Vector2Int, float> tileHealths;
    Tilemap t;
    void Start() {
        t = GetComponent<Tilemap>();
    }

    void Update() {
        
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        t.SetTile(t.WorldToCell(collision.GetContact(0).point + collision.GetContact(0).normal * 0.1f), null);
    }
}
