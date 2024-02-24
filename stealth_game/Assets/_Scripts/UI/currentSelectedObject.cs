using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentSelectedObject : MonoBehaviour {

    public Camera gameCamera;

    public string currentObject;


    // Start is called before the first frame update
    void Start() {
        currentObject = null;

    }

    // Update is called once per frame
    void Update() {


        // get mouse point on ground
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                // check if hit tile
                if (hitInfo.collider.gameObject.layer == 7) {
                    currentObject = hitInfo.collider.gameObject.GetComponent<TilePiece>().tileType;
                }
                else {
                    currentObject = hitInfo.collider.gameObject.name;
                }

            }
            else {
                currentObject = null;
            }
        }
    }

    public void setSelectedObject(GameObject selectedObject) {
        currentObject = selectedObject.name;
    }
}
