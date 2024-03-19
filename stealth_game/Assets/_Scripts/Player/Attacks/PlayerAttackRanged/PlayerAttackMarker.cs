using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMarker : MonoBehaviour {

    Camera camera;
   
    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
    }

    void Update() {

        //rotate to face mouse
        if (Input.GetKey(KeyCode.W)) { 
            GetComponent<Renderer>().enabled = true;
            FaceMouse();
        }
        else {
            GetComponent<Renderer>().enabled = false;
        }
    }

    void FaceMouse() {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance)) {
            Vector3 point = ray.GetPoint(rayDistance);
            transform.LookAt(point);
        }
    }
}
        


    
