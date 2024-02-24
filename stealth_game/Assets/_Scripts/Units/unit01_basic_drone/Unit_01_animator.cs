using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_01_animator : MonoBehaviour {

    Animator animator;
    Unit01 unit;
    public float blendTime;

    // Start is called before the first frame update
    void Start() {
        unit = GetComponent<Unit01>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float isMoving = unit.moving;
        animator.SetFloat("idle_switch", isMoving, blendTime, Time.deltaTime);
        
    }
}
