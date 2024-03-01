using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01ColllisionDetection : MonoBehaviour, IDamageable {

    float health;
    public float deathRadius = 8;
    public LayerMask enemyMask;

    public void TakeHit(float damage) {
        health -= damage;

        if (health <= 0) {
            
            // create array of other enemies within a certain range
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, deathRadius, enemyMask);

            // run enemy death method on all enemies within range
            foreach (Collider enemy in targetsInViewRadius) {

                gameObject.SetActive(false);
                IEnemyDeath otherEnemy = enemy.gameObject.GetComponent<IEnemyDeath>();
                if (otherEnemy != null) {
                    otherEnemy.EnemyDeath(gameObject.GetComponent<Unit01StateMachine>().currentCoord);

                    
                }
            }
            gameObject.SetActive(false);

        }
    }
}
