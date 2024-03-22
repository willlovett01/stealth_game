using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    PlayerStateMachine player;
    Animator animator;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerStateMachine>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        animationHandler();
    }

    void animationHandler() {
        if (player.IsMoving) {
            animator.SetBool("IsMoving", true);
        }
        else {
            animator.SetBool("IsMoving", false);
        }
        if (player.IsStealth) {
            animator.SetBool("IsStealth", true);
        }
        else {
            animator.SetBool("IsStealth", false);
        }
    }
}
