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
    bool rangedAttackOnCooldown;
    GameObject rangedAttackRadiusIndicator;
    GameObject rangedAttackRadiusArrow;

    GameObject targetObject;

    // Start is called before the first frame update
    void Start() {

        mouseOverObject = GameObject.Find("Currently_selected_object").GetComponent<currentSelectedObject>();

        rangedAttackRadiusIndicator = GameObject.Find("attack_ranged_radius_marker");
        rangedAttackRadiusArrow = GameObject.Find("attack_ranged_radius_arrow_marker");

        rangedAttackRadiusIndicator.SetActive(false);
        rangedAttackRadiusArrow.SetActive(false);

        targetObject = mouseOverObject.currentMouseOverObject;

        bombAttackOnCooldown = false;

    }

    // Update is called once per frame
    void Update() {

        // ranged attack
        targetObject = mouseOverObject.currentMouseOverObject;
        if (targetObject != null) {
            float arrowAngle = Vector3.Angle(transform.position, targetObject.transform.position);
        }

        // show and hide range indicator when key w is held down
        if (Input.GetKey(KeyCode.W)) {
            
            // enable UI for ranged attack
            rangedAttackRadiusIndicator.SetActive(true);
            rangedAttackRadiusArrow.SetActive(true);

            // rotate arrow UI to face target tile
            rangedAttackRadiusArrow.transform.LookAt(targetObject.transform);
            rangedAttackRadiusArrow.transform.eulerAngles += 180 * Vector3.up;

            // set color of position marker
            if (targetObject != null) {
                if (Vector3.Distance(transform.position, targetObject.transform.position) < rangedAttackRadius) {
                    GameObject.Find("Player_position_marker").GetComponent<Renderer>().material.SetInt("_player_attack_range_selected", 1);
                } else {
                    GameObject.Find("Player_position_marker").GetComponent<Renderer>().material.SetInt("_player_attack_range_selected", 0);
                    GameObject.Find("Player_position_marker").GetComponent<Renderer>().material.SetInt("_walkable", 0);
                }


            }

            // instantiate attack at selected tile if its within radius
            if (Input.GetMouseButtonDown(0)) {
                targetObject = mouseOverObject.currentMouseOverObject;
                
                if (targetObject != null) {

                    if (Vector3.Distance(transform.position, targetObject.transform.position) < rangedAttackRadius) {
                        StartCoroutine("RangedAttack", targetObject);
                    }
                }
            }
        }
            

        // reset if w is released
        if (Input.GetKeyUp("w")) {
            rangedAttackRadiusIndicator.SetActive(false);
            rangedAttackRadiusArrow.SetActive(false);
            GameObject.Find("Player_position_marker").GetComponent<Renderer>().material.SetInt("_player_attack_range_selected", 0);
        }
                

            
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

    // trigger ranged attack
    IEnumerator RangedAttack(GameObject target) {
        while (true) {

            GameObject newRanged = Instantiate(bombAttackPrefab, target.transform.position, Quaternion.identity);
            newRanged.transform.parent = GameObject.Find("Player_attack_ranged").transform;
            rangedAttackOnCooldown = true;
            yield return new WaitForSeconds(rangedAttackCooldown);
            rangedAttackOnCooldown = false;
            yield break;

        }
    }
}






