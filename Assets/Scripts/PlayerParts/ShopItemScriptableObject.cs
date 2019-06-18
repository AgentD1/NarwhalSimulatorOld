using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ShopItem", order = 1)]
public class ShopItemScriptableObject : ScriptableObject {
    public string itemName;
    public string briefDescription;
    public string longDescription;
    public Sprite shopThumbnail;
    public Sprite shopImage;
    public GameObject prefab;
    public string itemType;
    public int cost;
    public Color color;
}
