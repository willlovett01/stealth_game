using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour {
    int scoreLeft;
    public LevelLoadSettings levelSettings;


    void Start() {
        scoreLeft = 1;
    }

    void Update() {
        if (scoreLeft <= 0) {
            levelSettings.OnLevelWin();
        }
    }

    // reduce score by 1 when collectable collected
    public void ReduceScore() {
        scoreLeft -= 1;
    }
}

