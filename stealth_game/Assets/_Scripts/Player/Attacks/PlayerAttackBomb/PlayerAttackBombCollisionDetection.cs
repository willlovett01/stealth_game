using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBombCollisionDetection : MonoBehaviour {

    float damage = 1f;

    void OnTriggerEnter(Collider collision) {

        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null) {
            damageableObject.TakeHit(damage);
        }
    }
}
