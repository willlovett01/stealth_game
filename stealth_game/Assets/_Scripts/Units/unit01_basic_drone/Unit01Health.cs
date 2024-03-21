using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01Health : MonoBehaviour, IDamageable {

    float health = 2;
    public float deathRadius = 8;
    public LayerMask enemyMask;

    // when an enemy takes a hit from players attack
    public void TakeHit(float damage) {
        health -= damage;

        // check for first damage (enemy becomes stunned)
        if (health == 1) {

            // set state to stunned
            GetComponent<Unit01StateMachine>().IsStunned = true;
        }


        // check for second damage (enemy dies)
        else if (health <=0) {
            // create array of other enemies within a certain range
            Collider[] enemiesInSoundRadius = Physics.OverlapSphere(transform.position, deathRadius, enemyMask);

            // run enemy death method on all enemies within range (this is to check if theyre in range to hear the death)
            foreach (Collider enemy in enemiesInSoundRadius) {
                if (enemy != this.GetComponent<Collider>()) {
                    IMakeSound otherEnemy = enemy.gameObject.GetComponent<IMakeSound>();
                    if (otherEnemy != null) {
                        otherEnemy.MakeSound(gameObject.GetComponent<Unit01StateMachine>().currentCoord);
                    }
                }
            }

            // set state to stunned
            GetComponent<Unit01StateMachine>().IsDead = true;
        }
    }
}
                    

                
                
            
            


