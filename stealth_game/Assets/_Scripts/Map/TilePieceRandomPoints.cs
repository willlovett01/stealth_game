using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePieceRandomPoints : MonoBehaviour {

    public int pointCount;
    public List<Vector3> points;

    // Start is called before the first frame update
    void Awake() {
        List<Vector3> points = new List<Vector3>();
        GenerateRandomPoints();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void GenerateRandomPoints() {
        for (int i = 1; i < pointCount; i++) {
            Vector2 newPoint = Random.insideUnitCircle * 0.9f;
            Vector3 newPointNormalised = new Vector3(newPoint.x, 0, newPoint.y) + transform.position;
            points.Add(newPointNormalised);
            
        }
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.red;
    //    for (int i = 0; i < points.Count; i++) {
    //        Gizmos.DrawCube(points[i], Vector3.one * 0.1f);
    //    }
    //}
}
        
