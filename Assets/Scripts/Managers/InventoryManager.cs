using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager ins;

    public List<string> ownedItems;

    void Start() {
        ins = this;
        if (ownedItems == null) {
            ownedItems = new List<string>();
        }
    }

}
