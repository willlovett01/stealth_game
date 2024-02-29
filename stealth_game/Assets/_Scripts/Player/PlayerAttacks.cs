using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

    // bomb attack
    public float bombAttackCooldown;
    public GameObject bombAttackPrefab;
    bool bombAttackOnCooldown;

    // ranged attack
    public float rangedAttackRadius;
    public GameObject rangedAttackPrefab;
    bool rangedAttackOnCooldown;

    // Start is called before the first frame update
    void Start() {
        bombAttackOnCooldown = false;
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
            bombAttackOnCooldown = true;
            yield return new WaitForSeconds(bombAttackCooldown);
            bombAttackOnCooldown = false;
            yield break;

        }
    }
}




