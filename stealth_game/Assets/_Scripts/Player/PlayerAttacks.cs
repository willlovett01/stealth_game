using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {


    currentSelectedObject mouseOverObject;

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
    GameObject targetObject;

    // Start is called before the first frame update
    void Start() {

        mouseOverObject = GameObject.Find("Currently_selected_object").GetComponent<currentSelectedObject>();
        rangedAttackRadiusIndicator = GameObject.Find("attack_ranged_radius_marker");
        rangedAttackRadiusIndicator.SetActive(false);
        targetObject = mouseOverObject.currentMouseOverObject;

        bombAttackOnCooldown = false;

    }

    // Update is called once per frame
    void Update() {

        // ranged attack

        // show and hide range indicator when key w is held down
        if (Input.GetKey(KeyCode.W)) {
            rangedAttackRadiusIndicator.SetActive(true);
            if (Input.GetMouseButtonDown(0)) {
                targetObject = mouseOverObject.currentMouseOverObject;
                print("test");
                if (targetObject != null) {

                    if (Vector3.Distance(transform.position, targetObject.transform.position) < rangedAttackRadius) {
                        StartCoroutine("RangedAttack", targetObject);
                    }
                }
            }
        }

        if (Input.GetKeyUp("w")) {
            rangedAttackRadiusIndicator.SetActive(false);
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





