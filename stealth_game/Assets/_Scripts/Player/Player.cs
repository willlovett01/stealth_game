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


    TilePiece[] path;
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
                

            }
        }
   
    }


    public void onPathFound(TilePiece[] newPath, bool pathSuccessfull) {
        if (pathSuccessfull) {
            path = newPath;
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
            }

            currentTile = currentWaypoint;
 
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position + height, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint.transform.position + height);
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


    
    
    



   
    



               
                



                
