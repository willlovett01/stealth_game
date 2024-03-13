using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01Damage : MonoBehaviour, IDamageable {

    float health;
    public float deathRadius = 8;
    public LayerMask enemyMask;

    // when an enemy takes a hit from players attack
    public void TakeHit(float damage) {
        health -= damage;

        if (health <= 0) {
            
            // create array of other enemies within a certain range
            Collider[] enemiesInSoundRadius = Physics.OverlapSphere(transform.position, deathRadius, enemyMask);

            // run enemy death method on all enemies within range
            foreach (Collider enemy in enemiesInSoundRadius) {
                    IMakeSound otherEnemy = enemy.gameObject.GetComponent<IMakeSound>();
                    if (otherEnemy != null) {
                        otherEnemy.MakeSound(gameObject.GetComponent<Unit01StateMachine>().currentCoord);

                    }
            }
            gameObject.SetActive(false);
        }
    }
}
                    

                
                


