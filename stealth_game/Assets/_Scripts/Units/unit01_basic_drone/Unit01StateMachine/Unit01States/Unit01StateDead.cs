using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;

public class Unit01StateDead : Unit01BaseState {
    public Unit01StateDead(Unit01StateMachine currentContext, Unit01StateFactory unit01StateFactory)
    : base(currentContext, unit01StateFactory) { }

    IEnumerator stunned;

    public override void EnterState() {

        //hide vision cone
        ctx.VisionConeVisualiser.SetActive(false);

        // disable everything on death except mesh
        ctx.GetComponent<Unit01Animator>().enabled = false;
        ctx.GetComponent<Unit01FieldOfView>().enabled = false;
        ctx.GetComponent<Unit01StateMachine>().enabled = false;
        ctx.transform.Find("Enemy_unit01_graphic/Armature/drone_ctrl/unit_01_collider").GetComponent<SphereCollider>().enabled = false;


    }

    public override void UpdateState() { }
    
    public override void ExitState() { }

    public override void CheckSwitchStates() { }

    public override void InitialiseSubState() { }

}
        

        


        






