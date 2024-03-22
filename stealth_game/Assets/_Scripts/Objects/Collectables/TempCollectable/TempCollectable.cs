using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempCollectable : MonoBehaviour {

    public ScoreCounter scoreCounter;
    public GameObject map;
    TilePiece currentTile;

    private void Start() {
        currentTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();
        transform.position = new Vector3(currentTile.gameObject.transform.position.x, transform.position.y, currentTile.gameObject.transform.position.z);
    }

    private void OnTriggerEnter(Collider collider) {
        scoreCounter.ReduceScore();
        gameObject.SetActive(false);
    }
}

