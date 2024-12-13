using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public int level;
    public Text levelText;


    // level generation settings
    public string levelType;
    public static int levelMapRadius;
    public static float levelMapSeed;

    void Start () {

        // set text on button
        levelText.text = level.ToString();
    }

    public void OnOpenButtonClick() {

        // load into level
        loadLevel();
    }


    void loadLevel() {


        // LEVEL GENERATION

        // set and store level paramaters. They are set here so they are stored until level number is changed (in case someone dies they can re-do the same seed)
        if (levelType != "shop") {
            levelMapSeed = Random.Range(-200, 200);
            levelMapRadius = Random.Range(4, 9);

            SceneManager.LoadScene("Level_01");
        }
        // shop level settings
        else if (levelType == "shop") {
            levelMapSeed = -117;
            levelMapRadius = 5;

            SceneManager.LoadScene("Level_Shop");
        }
    }
}
            

