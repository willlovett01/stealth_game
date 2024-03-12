using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.VisualScripting.FullSerializer;

public class Unit01StatePatrolling : Unit01BaseState {

    public Unit01StatePatrolling(Unit01StateMachine currentContext, Unit01StateFactory unit01StateFactory)
    : base(currentContext, unit01StateFactory) {}

    IEnumerator followPath;

    public override void EnterState() {

        followPath = FollowPath();

        PathRequestManager.RequestPath(ctx.FirstTile, ctx.LastTile, onPathFound);

        
    }

    public override void UpdateState() {}

    public override void ExitState() {
        ctx.StopCoroutine(followPath);
        Debug.Log("end_patrolling");
    }

    public override void CheckSwitchStates() {}

    public override void InitialiseSubState() {}


    // generate list of tiles in a path from current to requested tiles
    public void onPathFound(TilePiece[] Path, bool pathSuccessfull) {
        if (pathSuccessfull) {
            ctx.Path = Path;
            ctx.TilePiecePositions = new List<Vector3>();

            foreach (TilePiece tilePiece in ctx.Path) {
                ctx.TilePiecePositions.Add(tilePiece.transform.position + new Vector3(0, 0.05f, 0));
            }

            // update line visual
            ctx.LineRenderer.positionCount = ctx.TilePiecePositions.Count;
            ctx.LineRenderer.SetPositions(ctx.TilePiecePositions.ToArray());

            ctx.StopCoroutine(followPath);
            ctx.StartCoroutine(followPath);
        }
    }

    // coroutine to follow path
    IEnumerator FollowPath() {

        TilePiece currentWaypoint = ctx.Path[0];
        ctx.TargetIndex = 0;
        Vector3 height = new Vector3(0, ctx.transform.position.y, 0);

        while (true) {
            ctx.currentCoord = ctx.Path[ctx.TargetIndex];
            if (ctx.transform.position == currentWaypoint.transform.position + height) {
                ctx.TargetIndex++;
                ctx.Moving = 0.0f; // used for animation

                if (ctx.TargetIndex >= ctx.Path.Length) {
                    Array.Reverse(ctx.Path);
                    ctx.TargetIndex = 0;
                    yield return new WaitForSeconds(3);
                }

                currentWaypoint = ctx.Path[ctx.TargetIndex];
                yield return ctx.StartCoroutine(Turn());
                yield return null;
            }

            ctx.CurrentTile = currentWaypoint;
            ctx.Moving = 1.0f; // used for animation

            ctx.transform.position = Vector3.MoveTowards(ctx.transform.position, currentWaypoint.transform.position + height, ctx.speed * Time.deltaTime);
            ctx.transform.LookAt(currentWaypoint.transform.position + height);

            yield return null;
        }
    }

    // coroutine to turn to face next waypoint
    IEnumerator Turn() {

        Vector3 directionToTarget = (ctx.Path[ctx.TargetIndex].transform.position - ctx.transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(ctx.transform.eulerAngles.y, targetAngle)) > 0.005) {
            float angle = Mathf.MoveTowardsAngle(ctx.transform.eulerAngles.y, targetAngle, ctx.turnSpeed * Time.deltaTime);
            ctx.transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }
}










