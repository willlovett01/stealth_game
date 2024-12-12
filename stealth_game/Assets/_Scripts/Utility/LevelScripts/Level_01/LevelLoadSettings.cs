using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadSettings : MonoBehaviour {
    public MapGeneratorHex mapGenerator;

    

    //randomise map
    private void OnEnable() {

        mapGenerator.seed = LevelLoader.levelMapSeed;
        mapGenerator.mapRadius = LevelLoader.levelMapRadius;
    }


    public void OnLevelWin() {
        ProgressManager.currentLevelProgress += 1;
        SceneManager.LoadScene("LevelSelect");
    }
}
