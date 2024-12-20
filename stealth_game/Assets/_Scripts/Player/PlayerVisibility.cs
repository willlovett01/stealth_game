using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour {

    TilePiece currentTile;

    public bool hidden;
    [SerializeField]
    bool soundOnCountdown;
    public LayerMask enemyMask;

    Vector3 positionHeight;

    GameObject playerModel;


    // Start is called before the first frame update
    void Start() {

        hidden = true;
        StartCoroutine(CheckIfHidden());
        playerModel = GameObject.Find("Player_model");
    }

    void Update() {
        positionHeight = new Vector3 (transform.position.x, 1.2f, transform.position.z);
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
        float noiseRange = gameObject.GetComponent<PlayerStateMachine>().PlayerNoiseLevel / 2f;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(positionHeight, noiseRange, enemyMask);

        // run enemy death method on all enemies within range
        foreach (Collider enemy in targetsInViewRadius) {

            IMakeSound enemyInRange = enemy.gameObject.transform.parent.parent.parent.parent.GetComponent<IMakeSound>();
            if (enemyInRange != null) {
                enemyInRange.MakeSound(gameObject.GetComponent<PlayerStateMachine>().CurrentTile);
            }

        }
        
    }
}
            







