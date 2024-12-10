using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldCamera : MonoBehaviour
{
    public LayerMask layerMask;
    void Update() {
        if (Input.GetMouseButtonDown(0)) { // if left button pressed...
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
       
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, layerMask)) {
                    SceneManager.LoadScene("Level_01");
                
            }
        }
    }
}
