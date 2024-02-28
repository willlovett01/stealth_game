using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiddenDisplay : MonoBehaviour {

    Player_visibility playerVisibility;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start() {
        playerVisibility = GameObject.Find("Player").GetComponent<Player_visibility>();
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

