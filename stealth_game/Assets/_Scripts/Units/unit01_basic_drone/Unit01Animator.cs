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
        float isMoving = unit.Moving;
        animator.SetFloat("idle_switch", isMoving, blendTime, Time.deltaTime);
        
    }
}
