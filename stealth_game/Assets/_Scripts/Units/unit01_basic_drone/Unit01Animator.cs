using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01Animator : MonoBehaviour {

    Animator animator;
    Unit01StateMachine unit;
    public float blendTime;

    // Start is called before the first frame update
    void Start() {
        unit = GetComponent<Unit01StateMachine>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        animationHandler();
    }

    void animationHandler() {

        // check for movement
        if (unit.Moving) {
            animator.SetBool("isMoving", true);
        }
        else {
            animator.SetBool("isMoving", false);
        }

        //check for stunned
        if (unit.IsStunned) {
            animator.SetBool("isStunned", true);
        }
        else {
            animator.SetBool("isStunned", false);
        }

        //check for dead
        if (unit.IsDead) {
            animator.SetBool("isDead", true);
        }
    }
}

        

