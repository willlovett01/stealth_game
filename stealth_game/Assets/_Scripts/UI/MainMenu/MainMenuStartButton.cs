using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuStartButton : MonoBehaviour {
    public void OnOpenButtonClick() {
        SceneManager.LoadScene("Level_01");
    }
}
