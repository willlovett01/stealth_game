using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;

public class UnitSpawn : MonoBehaviour {

    public float speed = 2.5f;
    public GameObject pathFinder;
    public GameObject map;

    TilePiece currentTile;
    TilePiece requestedTile;

    Vector3[] path;
    int targetIndex;



    // Start is called before the first frame update
    void Start() {
        currentTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();
        requestedTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();

        transform.position = new Vector3(currentTile.gameObject.transform.position.x, transform.position.y, currentTile.gameObject.transform.position.z);
        PathRequestManager.RequestPath(currentTile, requestedTile, onPathFound);

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
                    Array.Reverse(path);
                    targetIndex = 0;
                }
                currentWaypoint = new Vector3(path[targetIndex].x, transform.position.y, path[targetIndex].z);
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint);
            yield return null;

        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 3);

        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.red;
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





