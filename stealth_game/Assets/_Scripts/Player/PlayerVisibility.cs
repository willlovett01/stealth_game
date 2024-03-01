using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour {

    TilePiece currentTile;

    public bool hidden;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine("CheckIfHidden");
    }


    IEnumerator CheckIfHidden() {
        while (true) {

            yield return new WaitForSeconds(0.2f);

            currentTile = GetComponent<PlayerMovement>().currentTile;
            if (currentTile.tileType != "long_grass") {
                hidden = false;
                gameObject.layer = 8;
                GameObject.Find("Player_model").layer = 8;
            }
            else {
                hidden = true;
                gameObject.layer = 0;
                GameObject.Find("Player_model").layer = 0;
            }
        }
    }
}
            
