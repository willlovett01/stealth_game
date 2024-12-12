using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttacks : MonoBehaviour {


    public LayerMask enemyMask;

    // ranged attack
    public float rangedAttackCooldown;
    public int rangedAttackAmmo;
    public GameObject rangedAttackPrefab;
    bool rangedAttackOnCooldown;
    public Transform rangedAttackAim;

    // Update is called once per frame
    void Update() {      



        // arrow attack
        if (Input.GetKeyUp("w")) {
            if (rangedAttackOnCooldown == false) {
                if (rangedAttackAmmo != 0) {
                    StartCoroutine("RangedAttack");
                }
            }
        }
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
        
















