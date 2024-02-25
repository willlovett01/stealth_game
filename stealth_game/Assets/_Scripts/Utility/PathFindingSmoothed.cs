using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFindingSmoothed : MonoBehaviour {
    PathRequestManagerSmoothed requestManager;

    void Awake() {
        requestManager = GetComponent<PathRequestManagerSmoothed>();
    }

    public void StartFindPath(TilePiece startPos, TilePiece endPos) {
        StartCoroutine(FindPath(startPos, endPos));
    }


    IEnumerator FindPath(TilePiece startTile, TilePiece targetTile) {

        TilePiece[] waypoints = new TilePiece[0];
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

    TilePiece[] RetracePath(TilePiece startTile, TilePiece endTile) {

        List<TilePiece> path = new List<TilePiece>();
        TilePiece currentTile = endTile;

        path.Add(endTile);
        while (currentTile != startTile) {
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }
        path.Add(startTile);

        TilePiece[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    TilePiece[] SimplifyPath(List<TilePiece> path) {
        List<TilePiece> waypoints = new List<TilePiece>();

        for (int i = 1; i < path.Count; i++) {

            Vector3 prevDir = path[i].transform.position - path[Mathf.Clamp((i - 1), 0, path.Count - 1)].transform.position;
            Vector3 nextDir = path[i].transform.position - path[Mathf.Clamp((i + 1), 0, path.Count - 1)].transform.position;

            if (nextDir + prevDir != Vector3.zero) {
                waypoints.Add(path[i]);
            }
        }
        return waypoints.ToArray();
    }

    int getDistance(TilePiece tileA, TilePiece tileB) {
        int dstX = Mathf.Abs(tileA.offsetCoordinate.x - tileB.offsetCoordinate.x);
        int dstY = Mathf.Abs(tileA.offsetCoordinate.y - tileB.offsetCoordinate.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}


          



