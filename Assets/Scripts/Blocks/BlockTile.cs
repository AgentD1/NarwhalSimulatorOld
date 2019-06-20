using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BlockTile : Tile {
    public float Health;

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/BlockTile")]
    public static void CreateRoadTile() {
        string path = EditorUtility.SaveFilePanelInProject("Save Block Tile", "New Block Tile", "Asset", "Save Block Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BlockTile>(), path);
    }
#endif
}
