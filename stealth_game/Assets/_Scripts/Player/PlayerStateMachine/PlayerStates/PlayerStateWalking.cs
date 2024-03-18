using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using System.Linq;

public class PlayerStateWalking : PlayerStateBase {

    public PlayerStateWalking(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    IEnumerator followPath;

    public override void EnterState() {
        // set walking speed
        ctx.Speed = 8;
        ctx.PlayerNoiseLevel = 8f; 

        followPath = FollowPath();

        // start walking routine
        PathRequestManager.RequestPath(ctx.CurrentTile, ctx.RequestedTile, onPathFound);

    }

    public override void UpdateState() {
        CheckMouseClick();
        CheckForSneaking();
    }

    public override void ExitState() { }

    public override void CheckSwitchState() { }

    public override void InitialiseSubState() { }





    // request a path and start couroutine to follow it
    public void onPathFound(TilePiece[] newPath, bool pathSuccessfull) {
        if (pathSuccessfull) {
            ctx.TilePiecePositions = new List<Vector3>();
            ctx.Path = newPath;

            foreach (TilePiece tilePiece in ctx.Path) {
                ctx.TilePiecePositions.Add(tilePiece.transform.position + new Vector3(0, 0.05f, 0));
            }

            // update line visual
            ctx.LineRenderer.enabled = true;
            ctx.LineRenderer.positionCount = ctx.TilePiecePositions.Count;
            ctx.LineRenderer.SetPositions(ctx.TilePiecePositions.ToArray());

            //ctx.StopCoroutine(followPath);
            ctx.StopCoroutine(followPath);
            ctx.StartCoroutine(followPath);
        }
    }

    // follow path coroutine
    IEnumerator FollowPath() {
        TilePiece currentWaypoint = ctx.Path[0];
        ctx.TargetIndex = 0;
        Vector3 height = new Vector3(0, ctx.transform.position.y, 0);

        while (true) {
            if (ctx.transform.position == currentWaypoint.transform.position + height) {
                ctx.TargetIndex++;
                if (ctx.TargetIndex >= ctx.Path.Length) {
                    ctx.IsMoveRequested = false;
                    SwitchState(ctx.States.Idle());
                    yield break;
                }
                currentWaypoint = ctx.Path[ctx.TargetIndex];

            }

            ctx.CurrentTile = currentWaypoint;

            ctx.transform.position = Vector3.MoveTowards(ctx.transform.position, currentWaypoint.transform.position + height, ctx.Speed * Time.deltaTime);
            ctx.transform.LookAt(ctx.Path.Last().transform.position + height);
            yield return null;

        }
    }

    // check for mouse click, will update path to start a new one from current position
    void CheckMouseClick() {
        // get mouse point on ground
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = ctx.GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, ctx.layerMask)) {
                if (hitInfo.collider.GetComponent<TilePiece>() != null) {

                    // to reset target index
                    ctx.TargetIndex = 0;
                    
                    // request new path from current position
                    PathRequestManager.RequestPath(ctx.CurrentTile, ctx.RequestedTile, onPathFound);

                }
            }
        }
    }

    // check if control key is pressed 
    void CheckForSneaking() {
        if (ctx.IsStealth) {
            ctx.Speed = 3;
            ctx.PlayerNoiseLevel = 1;
        }
        
        else {
            ctx.Speed = 8;
            ctx.PlayerNoiseLevel = 8f;
        }
    }
}



