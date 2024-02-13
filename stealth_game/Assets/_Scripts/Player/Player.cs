using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {

    public float speed;

    public Transform groundObject;
    public Camera gameCamera;
    public LayerMask layerMask;
    public GameObject map;

    Vector3 requestedPosition;
    Vector3 velocity;
    float targetDirection;
    

    Rigidbody playerRigidBody;
    
    
    
    
    
    // Start is called before the first frame update
    void Start() {

        requestedPosition = transform.position;
        
        playerRigidBody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update() {

        // get mouse point on ground
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, layerMask)) {
                requestedPosition = hitInfo.point;
                requestedPosition = new Vector3(Mathf.Round(requestedPosition.x), transform.position.y, Mathf.Round(requestedPosition.z));
                print(requestedPosition);
                targetDirection = Mathf.Atan2(requestedPosition.x, requestedPosition.z) * Mathf.Rad2Deg;


               
                
            }
        }

        //move player to requested position
        if (transform.position != requestedPosition) {
            transform.position = Vector3.MoveTowards(transform.position, requestedPosition, speed * Time.deltaTime);
        }

        //rotate player
        transform.LookAt(requestedPosition);
   
    }

    void FixedUpdate() {

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 3);
    }

} 

   
    




                
