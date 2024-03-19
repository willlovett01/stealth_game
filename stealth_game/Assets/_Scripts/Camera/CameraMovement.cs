using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public float rotateSpeed;
    public XboxGamepadControls cameraControls;

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

        // mouse and keyboard
        if (Input.GetKey(KeyCode.A)) {
            transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed * Time.deltaTime);
            }
        if (Input.GetKey(KeyCode.D)) {
            transform.RotateAround(Vector3.zero, Vector3.up, -rotateSpeed * Time.deltaTime);
        }

        // controller
        transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed * Time.deltaTime * rotateCamera.ReadValue<float>()); // triggers
        transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed * Time.deltaTime * Input.GetAxis("Right Analog Stick (Horizontal)")); // right stick

    }
}
        
        




