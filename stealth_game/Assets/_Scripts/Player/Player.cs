using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {

    public float speed;

    public Camera gameCamera;
    public LayerMask layerMask;
    public GameObject pathFinder;
    public GameObject map;

    TilePiece requestedTile;
    TilePiece currentTile;


    Vector3[] path;
    int targetIndex;
    
    
    // Start is called before the first frame update
    void Start() {
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
                requestedTile = hitInfo.collider.GetComponent<TilePiece>();
                PathRequestManager.RequestPath(currentTile, requestedTile, onPathFound);
                currentTile = requestedTile;

            }
        }
   
    }


    public void onPathFound(Vector3[] newPath, bool pathSuccessfull) {
        if (pathSuccessfull) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = new Vector3(path[0].x, transform.position.y, path[0].z);
        targetIndex = 0;

        while (true) {
            if (transform.position == currentWaypoint) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = new Vector3(path[targetIndex].x,transform.position.y, path[targetIndex].z);
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint);
            yield return null;

        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 3);

        if(path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * 0.1f);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

} 


    
    
    



   
    



               
                



                
