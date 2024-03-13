using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01StateChasing : Unit01BaseState {
    public Unit01StateChasing(Unit01StateMachine currentContext, Unit01StateFactory unit01StateFactory)
    : base(currentContext, unit01StateFactory) { }



    public override void EnterState() {
        Debug.Log("isChasing");
    }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override void CheckSwitchStates() { }

    public override void InitialiseSubState() { }
}



