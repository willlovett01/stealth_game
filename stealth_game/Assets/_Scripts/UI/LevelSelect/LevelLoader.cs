using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public int level;
    public Text levelText;
    public static int levelMapRadius;
    public static float levelMapSeed;

    void Start () {

        // set text on button
        levelText.text = level.ToString();
    }

    public void OnOpenButtonClick() {

        // set and store level paramaters. They are set here so they are stored until level number is changed (in case someone dies they can re-do the same seed)
        levelMapSeed = Random.Range(-200, 200);
        levelMapRadius = Random.Range(4, 7);

        SceneManager.LoadScene("Level_01");
    }
}
