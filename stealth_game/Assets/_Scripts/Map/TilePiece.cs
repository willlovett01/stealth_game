using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class TilePiece : MonoBehaviour {

    Color originalColor;
    public string tileType;
    public bool clickable;

    public Vector2Int offsetCoordinate;
    public Vector3Int cubeCoordinate;
    public List<TilePiece> neighbours;
    public TilePiece parent;

    public int gCost;
    public int hCost;

    public int fCost() {
        return gCost + hCost;
    }

    public void IsClickable() {
        clickable = false;
    }

}





