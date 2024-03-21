using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerPositionMarker : MonoBehaviour {

    public Camera gameCamera;
    public LayerMask layerMask;
    public PlayerStateMachine player;

    // for path finding
    TilePiece[] path;
    List<Vector3> tilePiecePositions;
    LineRenderer lineRenderer;
    


    // Start is called before the first frame update
    void Awake() {
        lineRenderer = transform.Find("Path_render").GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layerMask)) {
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            if (hit.collider.GetComponent<TilePiece>() != null) {
                GetComponent<MeshRenderer>().material.SetInt("_walkable", Convert.ToInt32(hit.collider.GetComponent<TilePiece>().clickable));

                // get current and player tile pieces
                TilePiece currentTile = hit.collider.GetComponent<TilePiece>();
                TilePiece playerTile = player.CurrentTile;

                // move selection indicator to curser
                transform.position = hit.collider.transform.position;

                // draw path from player to line render
                if (hit.collider.GetComponent<TilePiece>().clickable) {

                    // check if player is aiming, if so hide the line
                    if (!Input.GetKey(KeyCode.W)) { 
                        PathRequestManager.RequestPath(currentTile, playerTile, onPathFound);
                    }
                } 
                else {
                    lineRenderer.enabled = false;
                }
            }
        }
        else {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            lineRenderer.enabled = false;
        }
    }


    public void onPathFound(TilePiece[] newPath, bool pathSuccessfull) {
        if (pathSuccessfull) {
            tilePiecePositions = new List<Vector3>();
            path = newPath;

            foreach (TilePiece tilePiece in path) {
                tilePiecePositions.Add(tilePiece.transform.position + new Vector3(0, 0.05f, 0));
            }

            //update line visual
            lineRenderer.enabled = true;
            lineRenderer.positionCount = tilePiecePositions.Count;
            lineRenderer.SetPositions(tilePiecePositions.ToArray());
        }
    }
}


            








