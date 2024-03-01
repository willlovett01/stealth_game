using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class Unit01Movement : MonoBehaviour, IEnemyDeath {

    public float speed = 2.5f;
    public int waitTime;
    public float turnSpeed;

    public GameObject pathFinder;
    public GameObject map;
    public GameObject currentlySelectedObject;

    LineRenderer lineRenderer;

    [SerializeField]
    TilePiece currentTile;
    TilePiece requestedTile;

    public TilePiece currentCoord;

    [SerializeField]
    TilePiece[] path;
    [SerializeField]
    TilePiece[] pathSave;
    List<Vector3> tilePiecePositions;

    int targetIndex;
    public float moving;

    TilePiece firstTile;
    TilePiece lastTile;



    // Start is called before the first frame update
    void Start() {

        lineRenderer = gameObject.transform.Find("UI").Find("Path_render").GetComponent<LineRenderer>();
        
        tilePiecePositions = new List<Vector3>();

        currentTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();
        requestedTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();
        firstTile = currentTile;
        lastTile = requestedTile;

        transform.position = new Vector3(currentTile.gameObject.transform.position.x, transform.position.y, currentTile.gameObject.transform.position.z);

        PathRequestManager.RequestPath(currentTile, requestedTile, onPathFound);
    }
       
    private void Update() {

        // set line visibility if object is selected
        if (currentlySelectedObject.GetComponent<currentSelectedObject>().currentObject == gameObject) {
            lineRenderer.enabled = true;
        }
        else {
            lineRenderer.enabled = false;
        }
    }

    public void onPathFound(TilePiece[] Path, bool pathSuccessfull) {
        if (pathSuccessfull) {
            path = Path;
            tilePiecePositions = new List<Vector3>();

            foreach (TilePiece tilePiece in path) {
                tilePiecePositions.Add(tilePiece.transform.position + new Vector3(0, 0.05f, 0));
            }


            // update line visual
            lineRenderer.positionCount = tilePiecePositions.Count;
            lineRenderer.SetPositions(tilePiecePositions.ToArray());

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    // based off enemy death interface, will run when enemy within certain range is found
    public void EnemyDeath(TilePiece deathLocation) {
        
        
    }


    public void onPathFoundOneWay(TilePiece[] Path, bool pathSuccessfull) {
        if (pathSuccessfull) {
            path = Path;
            tilePiecePositions = new List<Vector3>();

            foreach (TilePiece tilePiece in path) {
                tilePiecePositions.Add(tilePiece.transform.position + new Vector3(0, 0.05f, 0));
            }


            // update line visual
            lineRenderer.positionCount = tilePiecePositions.Count;
            lineRenderer.SetPositions(tilePiecePositions.ToArray());

            StopCoroutine("FollowPathOneWay");
            StartCoroutine("FollowPathOneWay");
        }
    }

    IEnumerator FollowPath() {
        TilePiece currentWaypoint = path[0];
        targetIndex = 0;
        Vector3 height = new Vector3(0, transform.position.y, 0);
 

        while (true) {
            currentCoord = path[targetIndex];
            if (transform.position == currentWaypoint.transform.position + height) {
                targetIndex++;
                moving = 0.0f; // used for animation

                if (targetIndex >= path.Length) {
                    Array.Reverse(path);
                    targetIndex = 0;
                    yield return new WaitForSeconds(3);
                }
                
                currentWaypoint = path[targetIndex];
                yield return StartCoroutine("Turn");
            }

            currentTile = currentWaypoint;
            moving = 1.0f; // used for animation

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position + height, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint.transform.position + height);

            yield return null;

        }
    }

    IEnumerator FollowPathOneWay() {
        TilePiece currentWaypoint = path[0];
        targetIndex = 0;
        Vector3 height = new Vector3(0, transform.position.y, 0);


        while (true) {
            currentCoord = path[targetIndex];
            if (transform.position == currentWaypoint.transform.position + height) {
                targetIndex++;
                moving = 0.0f; // used for animation

                if (targetIndex >= path.Length) {
                    yield break;

                }

                currentWaypoint = path[targetIndex];
                yield return StartCoroutine("Turn");
            }

            currentTile = currentWaypoint;
            moving = 1.0f; // used for animation

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position + height, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint.transform.position + height);

            yield return null;

        }
    }

    // investigation gets called if enemy is in range of another ddying, it goes to location of other enemy death, then retuns to original pathing
    IEnumerator Investigate(TilePiece deathLocation) {
        pathSave = path;
        StopCoroutine("FollowPath");
        // path to dead enemy location
        PathRequestManager.RequestPath(currentTile, deathLocation, onPathFoundOneWay);
        yield return null;
        yield return new WaitForSeconds(20);

        // path back to start of original path
        PathRequestManager.RequestPath(deathLocation, firstTile, onPathFoundOneWay);
        yield return new WaitForSeconds(20);

        // restart original path
        PathRequestManager.RequestPath(firstTile, lastTile, onPathFound);
        
    }



    IEnumerator Turn() {
        Vector3 directionToTarget = (path[targetIndex].transform.position - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.005) {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 3);

        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.red;
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

 














