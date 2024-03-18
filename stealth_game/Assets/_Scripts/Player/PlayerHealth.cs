using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    float health = 3;

    void Update() {
        if (health <= 0) {
            Death();
        }
    }

    // take damage
    public void TakeHit(float damage) {
        health -= damage;
    }

    void Death() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
