using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttacks : MonoBehaviour {


    public LayerMask enemyMask;

    // bomb attack
    public float bombAttackCooldown;
    public GameObject bombAttackPrefab;
    bool bombAttackOnCooldown;

    // ranged attack
    public float rangedAttackCooldown;
    public int rangedAttackAmmo;
    public GameObject rangedAttackPrefab;
    bool rangedAttackOnCooldown;
    public Transform rangedAttackAim;

    // Update is called once per frame
    void Update() {      

        // bomb attack
        if (Input.GetKeyDown("q")) {
            if (bombAttackOnCooldown == false) {
                StartCoroutine("BombAttack");
            }
        }

        // bomb attack
        if (Input.GetKeyUp("w")) {
            if (rangedAttackOnCooldown == false) {
                if (rangedAttackAmmo != 0) {
                    StartCoroutine("RangedAttack");
                }
            }
        }
    }


    // trigger bomb attack
    IEnumerator BombAttack() {
        
        // instantiate bomb
        GameObject newBomb = Instantiate(bombAttackPrefab,new Vector3(transform.position.x, 0 , transform.position.z), Quaternion.identity);
        newBomb.transform.parent = GameObject.Find("Player_attack_bomb").transform;
            

        // BOMB SOUND (triggers enemies within a certain radius to hear it go off)
        // create array of other enemies within a certain range
        Collider[] enemiesInSoundRadius = Physics.OverlapSphere(transform.position, 4, enemyMask);

        // run enemy death method on all enemies within range
        foreach (Collider enemy in enemiesInSoundRadius) {
            IMakeSound enemyInRange = enemy.GetComponent<IMakeSound>();
            if (enemyInRange != null) {
                enemyInRange.MakeSound(gameObject.GetComponent<PlayerStateMachine>().CurrentTile);
            }
        }
             
        // wait for cooldown
        bombAttackOnCooldown = true;
        yield return new WaitForSeconds(bombAttackCooldown);
        bombAttackOnCooldown = false;
        yield break;
    }

    IEnumerator RangedAttack() {

        // instantiate projectile
        GameObject newProjectile = Instantiate(rangedAttackPrefab, new Vector3(transform.position.x, 1.116f, transform.position.z), Quaternion.identity);
        newProjectile.transform.parent = GameObject.Find("Projectiles").transform;

        // set direction of projectile to projectile aim marker (not a fan of having to rely on the marker)
        newProjectile.transform.rotation = rangedAttackAim.rotation;

        // wait for cooldown
        rangedAttackAmmo -= 1;
        rangedAttackOnCooldown = true;
        yield return new WaitForSeconds(rangedAttackCooldown);
        rangedAttackOnCooldown = false;
        yield break;

    }
}
        
















