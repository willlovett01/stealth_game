using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour {

    public float moveSpeed;
    Rigidbody rb;
    Vector3 moveVelocity;
    Camera camera;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>(); 
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        Vector3 moveInput = new Vector3(Input.GetAxis("Left Analog Stick (Horizontal)"), 0, Input.GetAxis("Left Analog Stick (Vertical)"));
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    public void FixedUpdate() {
        // get camera vectors
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        // project forward and right vectors on horizontal plane ( y = 0 )
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // get world space direction to move
        Vector3 moveDir = forward * -Input.GetAxis("Left Analog Stick (Vertical)") + right * Input.GetAxis("Left Analog Stick (Horizontal)");

        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }
}
