using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Linq;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour {

    public float speed;

    public Camera gameCamera;
    public LayerMask layerMask;
    public GameObject pathFinder;
    public GameObject map;
    LineRenderer lineRenderer;

    TilePiece requestedTile;
    public TilePiece currentTile;

    TilePiece[] path;
    List<Vector3> tilePiecePositions;
    int targetIndex;
    
    // Start is called before the first frame update
    void Start() {

        lineRenderer = GameObject.Find("Path_render").GetComponent<LineRenderer>();
        tilePiecePositions = new List<Vector3>();
        currentTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();
        transform.position = new Vector3(currentTile.gameObject.transform.position.x, transform.position.y, currentTile.gameObject.transform.position.z);
        
    }

    // Update is called once per frame
    void Update() {

        // get mouse point on ground
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, layerMask)) {
                if (hitInfo.collider.GetComponent<TilePiece>() != null) {
                    requestedTile = hitInfo.collider.GetComponent<TilePiece>();
                    PathRequestManager.RequestPath(currentTile, requestedTile, onPathFound);
                }
            }
        }
    }


    public void onPathFound(TilePiece[] newPath, bool pathSuccessfull) {
        if (pathSuccessfull) {
            tilePiecePositions = new List<Vector3>();
            path = newPath;

            foreach(TilePiece tilePiece in path) {
                tilePiecePositions.Add(tilePiece.transform.position + new Vector3(0,0.05f,0));
            }

            // update line visual
            lineRenderer.enabled = true;
            lineRenderer.positionCount = tilePiecePositions.Count;
            lineRenderer.SetPositions(tilePiecePositions.ToArray());

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        TilePiece currentWaypoint =  path[0];
        targetIndex = 0;
        Vector3 height = new Vector3(0,transform.position.y,0);

        while (true) {
            if (transform.position == currentWaypoint.transform.position + height) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
                //yield return StartCoroutine("Turn");
            
            }

            currentTile = currentWaypoint;
 
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position + height, speed * Time.deltaTime);
            transform.LookAt(path.Last().transform.position + height);
            yield return null;

        }
    }

    IEnumerator Turn() {
        Vector3 directionToTarget = (path[targetIndex].transform.position - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.005) {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, 1000 * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 3);

        if(path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i].transform.position, Vector3.one * 0.1f);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i].transform.position);
                }
                else {
                    Gizmos.DrawLine(path[i - 1].transform.position, path[i].transform.position);
                }
            }
        }
    }
} 

    






    
    
    



   
    



               
                



                
