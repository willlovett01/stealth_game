using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackRangedProjectile : MonoBehaviour {

    float speed = 50;
    float distanceTraveled = 0;
    float maxDistance = 30;
    public LayerMask enemyLayerMask;

    void Update() {
        float moveDistance = speed * Time.deltaTime;
        distanceTraveled += moveDistance;

        // move projectile
        transform.Translate(Vector3.forward * moveDistance);

        if (distanceTraveled > maxDistance) {
            Destroy(gameObject);
        }
    }

    // when hitting something damageable
    void OnTriggerEnter(Collider collision) {

        // get damagable component based off collider (which is in a child folder attached to the bone so it moves with unit)
        IDamageable damageableObject = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<IDamageable>();
        if (damageableObject != null) {
            damageableObject.TakeHit(1f);

            // spear gets stuck in enemy and disable collider
            speed = 0;
            transform.parent = collision.gameObject.transform;
            gameObject.GetComponent<Collider>().enabled = false;

        }
    }
}



                





 