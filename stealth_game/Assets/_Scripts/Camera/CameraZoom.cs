using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public float zoomSpeed;
  

    // Update is called once per frame
    void Update() {
        // mouse and keyboard

        //zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if (transform.localPosition.z < -10) {
                transform.localPosition += new Vector3(0, 0, zoomSpeed);
            }
            
        }

        // zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            if (transform.localPosition.z > -30) {
                transform.localPosition += new Vector3(0, 0, -zoomSpeed);
            }
        }

    }
}
