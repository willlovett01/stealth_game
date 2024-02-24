using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy_panel : MonoBehaviour {


    public Button button;
    public GameObject enemy;
    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start() {
        selectedObject = GameObject.Find("Currently_selected_object");
        button = transform.Find("Select_enemy").GetComponent<Button>();

    }
    // Update is called once per frame
    public void Update() {
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick() {
        selectedObject.GetComponent<currentSelectedObject>().setSelectedObject(enemy);
    }

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  