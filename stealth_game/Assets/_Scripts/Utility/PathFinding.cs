using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;




public class PathFinding : MonoBehaviour {

    public GameObject map;
    public GameObject start, end;


    void Update() {
        FindPath(start.GetComponent<TilePiece>(), end.GetComponent<TilePiece>());
    }

    public void FindPath(TilePiece startTile, TilePiece targetTile) {

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
                RetracePath(startTile, targetTile);
                return;
            }

            foreach(TilePiece neighbour in currentTile.neighbours) {
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

    void RetracePath(TilePiece startTile,  TilePiece endTile) {
        List<TilePiece> path = new List<TilePiece>();
        TilePiece currentTile = endTile;

        while (currentTile != startTile) { 
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }
        path.Reverse();
        print(path);

        map.GetComponent<MapGeneratorHex>().path = path;

    }

    int getDistance(TilePiece tileA, TilePiece tileB) {
        int dstX = Mathf.Abs(tileA.offsetCoordinate.x - tileB.offsetCoordinate.x);
        int dstY = Mathf.Abs(tileA.offsetCoordinate.y - tileB.offsetCoordinate.y);

        if (dstX > dstY) 
            return 14*dstY + 10* (dstX - dstY);
        return 14*dstX + 10* (dstY - dstX);
    }
}

          



