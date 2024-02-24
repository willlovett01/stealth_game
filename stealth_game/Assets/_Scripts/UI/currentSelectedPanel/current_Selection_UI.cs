using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class current_Selection_UI : MonoBehaviour {

    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start() {
        selectedObject = GameObject.Find("Currently_selected_object");
    }

    // Update is called once per frame
    void Update() {
        transform.Find("Current_selection_text_UI").GetComponent<TextMeshProUGUI>().text = selectedObject.GetComponent<currentSelectedObject>().currentObject;
    }
}
 