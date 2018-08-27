using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FlexibleCellSize : MonoBehaviour {

    public float preferredWidth;
    public float preferredHeight;

    public bool affectWidth;
    public bool affectHeight;

    GridLayoutGroup gridLayout;
    RectTransform rectTransform;
    

    // Use this for initialization
    void Update() {
        if(gridLayout == null) {
            gridLayout = GetComponent<GridLayoutGroup>();
        }
        if(rectTransform == null) {
            rectTransform = GetComponent<RectTransform>();
        }
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        width -= gridLayout.padding.left;
        width -= gridLayout.padding.right;

        height -= gridLayout.padding.top;
        height -= gridLayout.padding.bottom;

        if (affectWidth) {
            gridLayout.cellSize = new Vector2(width / Mathf.Round(width / preferredWidth), gridLayout.cellSize.y);
        }

        if (affectHeight) {
            gridLayout.cellSize = new Vector2(gridLayout.cellSize.x, height / Mathf.Round(height / preferredHeight));
        }
    }
}
