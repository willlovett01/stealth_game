using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class currentSelectedObject : MonoBehaviour {

    public Camera gameCamera;

    public GameObject currentObject;
    public GameObject currentMouseOverObject;


    // Start is called before the first frame update
    void Start() {
        currentMouseOverObject = null;
        currentObject = null;

    }

    // Update is called once per frame
    void Update() {

        getMouseOverObject();
        if (Input.GetMouseButtonDown(0)) {
            getSelectedObject();
        }
    }





    public void setSelectedObject(GameObject selectedObject) {
        currentObject = selectedObject;
    }

    void getMouseOverObject() {
        // get object at mouse position
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)) {
            currentMouseOverObject = hitInfo.collider.gameObject;

        }
        else {
            currentObject = null;
        }
    }

    void getSelectedObject() {
        if (currentMouseOverObject != null) {
            currentObject = currentMouseOverObject;
        }
        else {
            currentObject = null;
        }
    }
}
        
        


        

