using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentSelectedObject : MonoBehaviour {

    public Camera gameCamera;

    public GameObject currentObject;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {


        // get mouse point on ground
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                currentObject = hitInfo.collider.gameObject;
                print(currentObject);

            }
            else {
                currentObject = null;
            }
        }
    }
}
