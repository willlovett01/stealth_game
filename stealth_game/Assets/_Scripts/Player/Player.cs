using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Linq;

public class Player : MonoBehaviour {

    public float speed;

    public Camera gameCamera;
    public LayerMask layerMask;
    public GameObject pathFinder;
    public GameObject map;
    public GameObject attackDisplay;
    public event Action onPlayerAOE;
    LineRenderer lineRenderer;

    TilePiece requestedTile;
    public TilePiece currentTile;


    TilePiece[] path;
    List<Vector3> tilePiecePositions;
    int targetIndex;
    
    
    // Start is called before the first frame update
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
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
                requestedTile = hitInfo.collider.GetComponent<TilePiece>();
                PathRequestManager.RequestPath(currentTile, requestedTile, onPathFound);

            }

        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine("Attack");
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
            }

            currentTile = currentWaypoint;
 
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position + height, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint.transform.position + height);
            yield return null;

        }
    }
   
    // basic AOE attack (temp)
    IEnumerator Attack() {
        Vector3 attackPos = new Vector3(currentTile.transform.position.x, 0.1f, currentTile.transform.position.z);
        GameObject attackAnim = Instantiate(attackDisplay, attackPos, Quaternion.Euler(Vector3.right * -90));
        attackAnim.transform.position = currentTile.gameObject.transform.position; 
        yield return new WaitForSeconds(1);
        Destroy(attackAnim);


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

    






    
    
    



   
    



               
                



                
