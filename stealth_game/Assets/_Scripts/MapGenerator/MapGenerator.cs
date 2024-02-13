using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform[] tilePrefab ;
    public float seed = 10;

    public float tileSize;
    public Vector2 mapSize;

    [Range(0, 100)]
    public float noiseDetail;

    [Range(0,1)]
    public float outlinePercent;

    void Start () {

        GenerateMap();
    }


    // map generator
    public void GenerateMap() {

       
        
        // create parent object for map
        string holderName = "Generated Map";
        if (GameObject.Find(holderName)) {
            DestroyImmediate(GameObject.Find(holderName));
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        // create map grid 
        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {
                Vector3 tilePosition = CoordToPosition(x, y);
                // instantiate tile
                Transform newTile = Instantiate(tilePrefab[GenerateNoise(x, y, noiseDetail)], tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;

                // scale each piece to add outlines for visual clarity
                newTile.localScale *= (1 - outlinePercent);
                newTile.parent = mapHolder;

                // lower water tiles (temp test)
                if (newTile.name == "Tile_03(Clone)") {
                    newTile.transform.position -= Vector3.up * 0.5f;
                }

                
            }
        }
    }

    // generate noise
    int GenerateNoise(int x, int z, float detailScale) {

        float xNoise = (x + this.transform.position.x + seed) / detailScale;
        float zNoise = (z + this.transform.position.y + seed) / detailScale;

        return (int)(Mathf.PerlinNoise(xNoise, zNoise)*tilePrefab.Length);
    }



    // generate tile position based on its coordinate
    Vector3 CoordToPosition(int x, int y) {
        return new Vector3(-(mapSize.x/2 * tileSize) + (x * tileSize) + tileSize/2 ,0 , -(mapSize.y / 2 * tileSize) + (y * tileSize) + tileSize / 2);
    }

}



  


