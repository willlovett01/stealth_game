using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour {

    // master level progress keeper. Will update when a level is beaten. Drives what levels are available
    public static int currentLevelProgress = 0;
    public Button[] levelSelectButtons;



    // Start is called before the first frame update
    void Start() {
        

        for (int i = 0; i < levelSelectButtons.Length; i++) {

            if (i != currentLevelProgress) {
                levelSelectButtons[i].interactable = false;
            }
        }   
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
