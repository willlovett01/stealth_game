using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit01StateChasing : Unit01BaseState {
    public Unit01StateChasing(Unit01StateMachine currentContext, Unit01StateFactory unit01StateFactory)
    : base(currentContext, unit01StateFactory) { }


    // coroutines
    IEnumerator shooting;

    Transform playerPosition;

    public override void EnterState() {

        // assign coroutines
        shooting = Shooting();

        // make red dot apear over enemies head
        ctx.ChasingVisualiser.SetActive(true);

        playerPosition = ctx.Player.transform;

        // start routine
        ctx.StartCoroutine(shooting);

    }

    public override void UpdateState() { 
        playerPosition = ctx.Player.transform;
        ctx.Gun.Shoot();

        if (ctx.Stunned == true) {
            SwitchState(factory.Stunned());
        }
    }

    public override void ExitState() { 

        
        ctx.ChasingVisualiser.SetActive(false);

        ctx.StopCoroutine(shooting);
        shooting = Shooting();
        

        
    }

    public override void CheckSwitchStates() { }

    public override void InitialiseSubState() { }


    // currently just stands and looks at player, need to add more logic
    IEnumerator Shooting() {

        // get distance to player
        float dist = Vector3.Distance(playerPosition.position, ctx.transform.position);

        // while in range of player shoot (currently entire map)
        while (dist < 30f) {

            // recalculate distance
            dist = Vector3.Distance(playerPosition.position, ctx.transform.position);

            Vector3 directionToTarget = (playerPosition.position - ctx.transform.position).normalized;
            float targetAngle = 90 - Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

            while (Mathf.Abs(Mathf.DeltaAngle(ctx.transform.eulerAngles.y, targetAngle)) > 0.005) {
                float angle = Mathf.MoveTowardsAngle(ctx.transform.eulerAngles.y, targetAngle, 60 * Time.deltaTime);
                ctx.transform.eulerAngles = Vector3.up * angle;
                yield return null;
            }

            yield return null;
        }
    }
}
        


            




