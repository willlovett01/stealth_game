using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGeneratorHex))]
public class MapEditorHex : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUI.changed) {
            MapGeneratorHex map = target as MapGeneratorHex;

            map.GenerateMap();
        }
    }

}
