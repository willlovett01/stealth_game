using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerRB : MonoBehaviour {

    public float speed;
    public Camera gameCamera;
    public LayerMask layerMask;

    Vector3 requestedPosition;
    Vector3 requestedTile;
    Vector3 requestedPositionLocal;

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
                if (hitInfo.collider.GetComponent<TilePiece>()) {
                    if (hitInfo.collider.GetComponent<TilePiece>().clickable == true) {

                        requestedPosition = hitInfo.point;
                        requestedTile = hitInfo.collider.transform.position;


                        print(requestedTile);
                        //requestedPosition = new Vector3(Mathf.Round(requestedPosition.x), transform.position.y, Mathf.Round(requestedPosition.z));
                        requestedPosition = new Vector3(requestedTile.x, transform.position.y, requestedTile.z);

                        requestedPositionLocal = requestedTile - transform.position;
                        targetDirection = Mathf.Atan2(requestedPositionLocal.x, requestedPositionLocal.z) * Mathf.Rad2Deg;
                    }
                }
            }
        }
    }

    void FixedUpdate() {
        playerRigidBody.MoveRotation(Quaternion.Euler(Vector3.up * targetDirection));
        playerRigidBody.MovePosition(Vector3.MoveTowards(transform.position, requestedPosition, speed * Time.fixedDeltaTime));
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 3);    
    }

} 
            

    

                



                

   
    




                
