using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRadiusMarker : MonoBehaviour {

    // references
    PlayerStateMachine playerStateMachine;
    float soundLevel;

    // Start is called before the first frame update
    void Start() {

        // get reference to player state machine to access player noise level
        playerStateMachine = GameObject.Find("Player_main").GetComponent<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update() {

        // update radius of marker
        soundLevel = playerStateMachine.PlayerNoiseLevel;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1 * soundLevel,1, 1 * soundLevel), 10 * Time.deltaTime);
    }

}

