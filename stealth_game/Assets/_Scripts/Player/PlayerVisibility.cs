using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour {

    TilePiece currentTile;

    public bool hidden;
    [SerializeField]
    bool soundOnCountdown;
    public LayerMask enemyMask;

    GameObject playerModel;


    // Start is called before the first frame update
    void Start() {
   
        StartCoroutine(CheckIfHidden());
        playerModel = GameObject.Find("Player_model");
    }

    void Update() {
        CheckInRangeOfNoise();
    }

    IEnumerator CheckIfHidden() {
        while (true) {

            yield return new WaitForSeconds(0.2f);

            currentTile = GetComponent<PlayerStateMachine>().CurrentTile;
            if (currentTile.tileType != "long_grass") {
                hidden = false;
                gameObject.layer = 8;
                playerModel.layer = 8;
            }
            else {
                hidden = true;
                gameObject.layer = 10;
                playerModel.layer = 10;
            }
        }
    }

    void CheckInRangeOfNoise() {
        float noiseRange = gameObject.GetComponent<PlayerStateMachine>().PlayerNoiseLevel / 2f + 1f;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, noiseRange, enemyMask);

        // run enemy death method on all enemies within range
        foreach (Collider enemy in targetsInViewRadius) {
            IMakeSound enemyInRange = enemy.GetComponent<IMakeSound>();
            if (enemyInRange != null) {
                Debug.DrawLine(transform.position, enemy.gameObject.transform.position);
                enemyInRange.MakeSound(gameObject.GetComponent<PlayerStateMachine>().CurrentTile);
            }

        }
        
    }
}
            







