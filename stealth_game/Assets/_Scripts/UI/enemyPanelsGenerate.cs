using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemyPanelsGenerate : MonoBehaviour {

    public GameObject enemyPanel;
    public Transform enemies;

    // Start is called before the first frame update
    void Awake() {
        for(int i = 0; i < enemies.childCount; i++) {
            generateEnemyPanel(enemies.GetChild(i), i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateEnemyPanel(Transform enemy, int enemyIndex) {
        GameObject newPanel = Instantiate(enemyPanel);
        newPanel.transform.parent = transform;
        newPanel.GetComponent<RectTransform>().localPosition = new Vector3(50 ,640 - (80 * enemyIndex), 0);
        newPanel.GetComponent<enemy_panel>().enemy = enemy.gameObject;

        newPanel.transform.Find("Enemy_name").GetComponent<TextMeshProUGUI>().text = enemy.name;
    }
}
