using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiddenDisplay : MonoBehaviour {

    public PlayerVisibility playerVisibility;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (playerVisibility) {
            if (playerVisibility.hidden == false) {
                text.text = "visible";
            }
            else {
                text.text = "Hidden";
            }
        }
    }
}


