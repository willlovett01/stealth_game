using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;




public class PathFinding : MonoBehaviour {

    PathRequestManager requestManager;

    void Awake() {
        requestManager = GetComponent<PathRequestManager>();
    }

    public void StartFindPath(TilePiece startPos, TilePiece endPos) {
        StartCoroutine(FindPath(startPos, endPos));
    }


    IEnumerator FindPath(TilePiece startTile, TilePiece targetTile) {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        if (startTile.clickable && targetTile.clickable) {
            List<TilePiece> openSet = new List<TilePiece>();
            HashSet<TilePiece> closedSet = new HashSet<TilePiece>();
            openSet.Add(startTile);

            while (openSet.Count > 0) {
                TilePiece currentTile = openSet[0];
                for (int i = 1; i < openSet.Count; i++) {
                    if (openSet[i].fCost() < currentTile.fCost() ||
                        openSet[i].fCost() == currentTile.fCost() &&
                        openSet[i].hCost < currentTile.hCost) {
                        currentTile = openSet[i];
                    }
                }
                openSet.Remove(currentTile);
                closedSet.Add(currentTile);

                if (currentTile == targetTile) {
                    pathSuccess = true;
                    break;
                }

                foreach (TilePiece neighbour in currentTile.neighbours) {
                    if (closedSet.Contains(neighbour)) {
                        continue;
                    }

                    int newMovementCostToNeigbour = currentTile.gCost + getDistance(currentTile, neighbour);
                    if (newMovementCostToNeigbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                        neighbour.gCost = newMovementCostToNeigbour;
                        neighbour.hCost = getDistance(neighbour, targetTile);
                        neighbour.parent = currentTile;

                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        yield return null;
        if (pathSuccess) {
            waypoints = RetracePath(startTile, targetTile);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(TilePiece startTile,  TilePiece endTile) {
        print("start" + startTile);
        print("end" + endTile);
        List<TilePiece> path = new List<TilePiece>();
        TilePiece currentTile = endTile;

        path.Add(endTile);
        while (currentTile != startTile) { 
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }
        path.Add(startTile);
        for (int i = 0; i < path.Count; i++) {
            print(path[i]);
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    Vector3[] SimplifyPath(List<TilePiece> path) {
        List<Vector3> waypoints = new List<Vector3>();  
        Vector2 directionOld = Vector2.zero;

        // path smoothing currently disabled
        //for (int i = 1; i < path.Count; i++) {
        //    Vector2 directionNew = new Vector2(path[i - 1].offsetCoordinate.x - path[i].offsetCoordinate.x, path[i - 1].offsetCoordinate.y - path[i].offsetCoordinate.y);
        //    if (directionNew != directionOld) {
        //        waypoints.Add(path[i].transform.position);
        //        directionOld = directionNew;
        //    }
        //}
        for (int i = 1; i < path.Count; i++) {
            waypoints.Add(path[i].transform.position);
            
        }
        return waypoints.ToArray();
    }

    int getDistance(TilePiece tileA, TilePiece tileB) {
        int dstX = Mathf.Abs(tileA.offsetCoordinate.x - tileB.offsetCoordinate.x);
        int dstY = Mathf.Abs(tileA.offsetCoordinate.y - tileB.offsetCoordinate.y);

        if (dstX > dstY) 
            return 14*dstY + 10* (dstX - dstY);
        return 14*dstX + 10* (dstY - dstX);
    }
}

          



