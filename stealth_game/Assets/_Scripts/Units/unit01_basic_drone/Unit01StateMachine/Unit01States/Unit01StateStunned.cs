using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class Unit01StateStunned : Unit01BaseState {
    public Unit01StateStunned(Unit01StateMachine currentContext, Unit01StateFactory unit01StateFactory)
    : base(currentContext, unit01StateFactory) { }

    IEnumerator stunned;

    public override void EnterState() {

        stunned = Stunned();

        //hide vision cone
        ctx.VisionConeVisualiser.SetActive(false);
        ctx.Moving = false; // used for animation

        ctx.StartCoroutine(stunned);
    }

    public override void UpdateState() {

        // state switches
        if (ctx.IsDead == true) {
            SwitchState(factory.Dead());
        }
    }
    
    public override void ExitState() {

        ctx.StopCoroutine(stunned);
        ctx.VisionConeVisualiser.SetActive(true);
        stunned = Stunned();

        // reset see player so unit doesnt go straight back into shooting
        ctx.SeePlayer = false;
    }

    public override void CheckSwitchStates() { }

    public override void InitialiseSubState() { }

    IEnumerator Stunned() {

        yield return new WaitForSeconds(8);
        ctx.IsStunned = false;
        SwitchState(factory.Investigating());
        
    }
}
        

        


        






