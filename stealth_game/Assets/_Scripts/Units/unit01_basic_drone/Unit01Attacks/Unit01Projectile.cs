using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01Projectile : MonoBehaviour {

    float speed = 10;

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
}
