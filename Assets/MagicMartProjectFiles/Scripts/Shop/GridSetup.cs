using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSetup : MonoBehaviour
{
    [SerializeField] RectTransform prefabTile;
    GridLayoutGroup grid;

    void Awake()
    {
        float width = prefabTile.sizeDelta.x;
        float height = prefabTile.sizeDelta.y;
        grid.cellSize = new Vector2(width, height);
    }

}
