using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPanel : MonoBehaviour {


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

        // only works for specific enemy type, will need to expand or rethink when i add more enemies
        if (enemy.GetComponent<Unit01StateMachine>().IsDead ) {
            OnEnemyDeath();
        }
        
    }

    public void OnButtonClick() {
        selectedObject.GetComponent<currentSelectedObject>().setSelectedObject(enemy);
    }

    // delete panel if corrisponding enemy dies
    void OnEnemyDeath() {
        Destroy(this.gameObject);
    }

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  