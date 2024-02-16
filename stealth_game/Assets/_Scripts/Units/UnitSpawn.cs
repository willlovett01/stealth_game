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

    TilePiece[] path;
    int targetIndex;



    // Start is called before the first frame update
    void Start() {
        currentTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();
        requestedTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();

        transform.position = new Vector3(currentTile.gameObject.transform.position.x, transform.position.y, currentTile.gameObject.transform.position.z);
        PathRequestManagerSmoothed.RequestPath(currentTile, requestedTile, onPathFound);

    }

    public void onPathFound(TilePiece[] Path, bool pathSuccessfull) {
        if (pathSuccessfull) {
            path = Path;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        TilePiece currentWaypoint = path[0];
        targetIndex = 0;
        Vector3 height = new Vector3(0, transform.position.y, 0);

        while (true) {
            if (transform.position == currentWaypoint.transform.position + height) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    Array.Reverse(path);
                    targetIndex = 0;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position + height, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint.transform.position + height);
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




