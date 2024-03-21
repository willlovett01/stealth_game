using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01Projectile : MonoBehaviour {

    // projectile settings
    float speed = 10;
    float distanceTraveled = 0;
    float maxDistance = 20;

    public LayerMask playerVisibleMask;
    public LayerMask playerInvisibleMask;
    public PlayerHealth playerHealth;

    // Update is called once per frame


    void Update() {

        float moveDistance = speed * Time.deltaTime;
        distanceTraveled += moveDistance;

        // check for collision with player
        CheckCollisions(moveDistance); 

        // move projectile
        transform.Translate(Vector3.forward * moveDistance);

        if (distanceTraveled > maxDistance) {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    void CheckCollisions(float moveDistance) {
        Ray ray = new Ray (transform.position, transform.forward);
        RaycastHit hit;

        // need to come up with better way of checking multiple layers (look up bit flip)
        if(Physics.Raycast(ray, out hit, moveDistance, playerVisibleMask, QueryTriggerInteraction.Collide)) {
            Collider collision = hit.collider;
            if (collision != null) {
                playerHealth.TakeHit(1);
                Destroy(gameObject);
            }
        }

        if (Physics.Raycast(ray, out hit, moveDistance, playerInvisibleMask, QueryTriggerInteraction.Collide)) {
            Collider collision = hit.collider;
            if (collision != null) {
                playerHealth.TakeHit(1);
                Destroy(gameObject);
            }
        }
    }
}

    




