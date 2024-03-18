using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour {
    int scoreLeft;

    void Start() {
        scoreLeft = 3;
    }

    void Update() {
        if (scoreLeft <= 0) {
            resetScene();
        }
    }

    // reduce score by 1 when collectable collected
    public void ReduceScore() {
        scoreLeft -= 1;
    }

    void resetScene () {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

