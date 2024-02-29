using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentSelectionUI : MonoBehaviour {

    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start() {
        selectedObject = GameObject.Find("Currently_selected_object");
    }

    // Update is called once per frame
    void Update() {
        transform.Find("Current_selection_text_UI").GetComponent<TextMeshProUGUI>().text = getObjectName();
    }


    string getObjectName() {
        GameObject currentSelection = selectedObject.GetComponent<currentSelectedObject>().currentObject;
        if (currentSelection != null) {
            if (currentSelection.layer == 7) {
                return currentSelection.GetComponent<TilePiece>().tileType;
            }
            else {
                return currentSelection.name;
            }
        }
        return null;
    }
    
}
 

