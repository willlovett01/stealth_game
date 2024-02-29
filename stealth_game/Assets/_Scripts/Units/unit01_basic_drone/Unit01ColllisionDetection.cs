using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01ColllisionDetection : MonoBehaviour, IDamageable {

    float health;

    public void TakeHit(float damage) {
        health -= damage;

        if (health <= 0) {
            gameObject.SetActive(false);
        }
    }
}
