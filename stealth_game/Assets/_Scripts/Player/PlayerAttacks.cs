using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttacks : MonoBehaviour {


    currentSelectedObject mouseOverObject;

    public LayerMask enemyMask;

    // bomb attack
    public float bombAttackCooldown;
    public GameObject bombAttackPrefab;
    bool bombAttackOnCooldown;

    // ranged attack
    public float rangedAttackCooldown;
    public float rangedAttackRadius;
    public GameObject rangedAttackPrefab;
    GameObject rangedAttackRadiusIndicator;
    GameObject rangedAttackRadiusArrow;

    GameObject targetObject;

    
    void Start() {

        mouseOverObject = GameObject.Find("Currently_selected_object").GetComponent<currentSelectedObject>();
        targetObject = mouseOverObject.currentMouseOverObject;
    }

    // Update is called once per frame
    void Update() {      

            
        // bomb attack
        if (Input.GetKeyDown("q")) {
            if (bombAttackOnCooldown == false) {
                StartCoroutine("BombAttack");
            }
        }
    }

    // trigger bomb attack
    IEnumerator BombAttack() {
        while (true) {

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
                    


            bombAttackOnCooldown = true;
            yield return new WaitForSeconds(bombAttackCooldown);
            bombAttackOnCooldown = false;
            yield break;

        }
    }
}










