using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiddenDisplay : MonoBehaviour {

    PlayerVisibility playerVisibility;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start() {
        playerVisibility = GameObject.Find("Player_main").GetComponent<PlayerVisibility>();
        text = transform.Find("Hidden_display_text_UI").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        if (playerVisibility.hidden == false) {
            text.text = "visible";
        }
        else {
            text.text = "Hidden";
        }
    }
}

