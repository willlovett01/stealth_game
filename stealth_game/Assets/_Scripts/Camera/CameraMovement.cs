using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // follow player (currently disabled)
        //transform.position = player.position + new Vector3(-10,10,-10);

        if (Input.GetKey(KeyCode.A)) {
            transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed * Time.deltaTime);
            }
        if (Input.GetKey(KeyCode.D)) {
            transform.RotateAround(Vector3.zero, Vector3.up, -rotateSpeed * Time.deltaTime);
        }

    }

}
