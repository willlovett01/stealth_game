using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01Gun : MonoBehaviour {

    public Unit01Projectile projectile;
    public PlayerHealth playerHealth;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    float nextShotTime;

    public void Shoot() {

        if (Time.time > nextShotTime) {
            nextShotTime = Time.time + msBetweenShots/1000;
            // instantiate new bullets
            Unit01Projectile newProjectile = Instantiate(projectile, transform.position, transform.rotation) as Unit01Projectile;
            newProjectile.playerHealth = playerHealth;

            // set new bullet speed
            newProjectile.SetSpeed(muzzleVelocity);
            newProjectile.transform.parent = GameObject.Find("Projectiles").transform;
        }
    }
}
