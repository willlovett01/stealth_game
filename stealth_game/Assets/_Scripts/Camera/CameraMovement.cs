using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour {


    public float rotateSpeed;
    public Transform player;
    public XboxGamepadControls cameraControls;

    [SerializeField]
    bool cameraFollow;

    Vector3 followObject;

    //controls
    InputAction rotateCamera;

    private void Awake() {
        cameraControls = new XboxGamepadControls();
    }

    private void OnEnable() {
        rotateCamera = cameraControls.Camera.RotateCamera;
        rotateCamera.Enable();
    }

    private void OnDisable() {
        rotateCamera.Disable();
    }

    // Update is called once per frame
    void Update() {


        // check if camera is set to follow player or not
        checkForCameraToggle();
        if (cameraFollow) {
            followObject = player.position;
        }
        else {
            followObject = Vector3.zero;
        }


        // set camera position
        transform.position = followObject;

        // mouse and keyboard

        // rotation
        if (Input.GetKey(KeyCode.A)) {
            transform.RotateAround(followObject, Vector3.up, rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.RotateAround(followObject, Vector3.up, -rotateSpeed * Time.deltaTime);
        }

        // angle
        if (Input.GetKey(KeyCode.E)) {
            if (transform.rotation.eulerAngles.x < 85) {
                transform.Rotate(0.5f, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.Q)) {
            if (transform.rotation.eulerAngles.x > 15) {
                transform.Rotate(-0.5f, 0, 0);
            }
        }

        // check if follow player toggle is enbled. If so camera origin will be player, if not camera origin will be 0,0,0 
        void checkForCameraToggle() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (!cameraFollow) {
                    cameraFollow = true;
                }
                else {
                    cameraFollow = false;
                }
            }
        }
    }
}
        
                
                



        




