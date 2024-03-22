using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01LoadSettings : MonoBehaviour {
    public MapGeneratorHex mapGenerator;

    //randomise map
    private void OnEnable() {
        mapGenerator.seed = Random.Range(-200,200);
    }
}
