using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class TilePiece : MonoBehaviour {

    Color originalColor;
    public string tileType;
    public bool clickable;

    // Start is called before the first frame update
    void Start() {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    void OnMouseOver() {
        if (clickable) {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    void OnMouseExit() {
        if (clickable) {
            gameObject.GetComponent<Renderer>().material.color = originalColor;
        }

    }

    public void IsClickable() {
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        clickable = false;
    }

}



