using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempCollectable : MonoBehaviour {

    public ScoreCounter scoreCounter;

    private void OnTriggerEnter(Collider collider) {
        scoreCounter.ReduceScore();
        gameObject.SetActive(false);
    }
}

