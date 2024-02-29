using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionMarker : MonoBehaviour {

    public Camera gameCamera;
    public LayerMask layerMask;
    

    // Start is called before the first frame update
    void Start() {
        
    }


    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layerMask)) {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.position = hit.collider.transform.position;
            if (hit.collider.GetComponent<TilePiece>() != null) {
                GetComponent<MeshRenderer>().material.SetInt("_walkable", Convert.ToInt32(hit.collider.GetComponent<TilePiece>().clickable));
            }
        }
        else {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}


            
