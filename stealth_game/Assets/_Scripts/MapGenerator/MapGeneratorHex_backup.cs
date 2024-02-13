using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGeneratorHex_backup : MonoBehaviour {

    //map pieces 
    public Transform[] tilePrefab ;
    public Transform treePrefab;
    public int treeCount;

    // map controls
    public float seed = 10;
    public float mapRadius;
    public float tileSize;
    public Vector2 mapSize;


    [Range(1f, 100)]
    public float noiseDetail;

    [Range(0,1)]
    public float outlinePercent;

    List<Vector3> allGroundTilePositions;
    Queue<Vector3> allGroundTilePositionsShuffled;

    float width;
    float height;
    float offset;
    float mapOffsetx;
    float mapOffsety;
    Vector3 distanceToCenter;

    void Start () {

        GenerateMap();
    }

    // map generator
    public void GenerateMap() {

        allGroundTilePositions = new List<Vector3>();

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
                distanceToCenter = Vector3.zero - tilePosition;

                // only instantiate tiles within a certain distance to center (to create circle shape)
                if (distanceToCenter.magnitude < mapRadius) {

                    // instantiate tile
                    Transform newTile = Instantiate(tilePrefab[GenerateNoise(x, y, noiseDetail)], tilePosition, Quaternion.Euler(Vector3.right * -90)) as Transform;

                    string newTileType = newTile.gameObject.GetComponent<TilePiece>().tileType;

                    // scale each piece to add outlines for visual clarity
                    newTile.localScale *= tileSize;
                    newTile.localScale *= (1 - outlinePercent);
                    newTile.parent = mapHolder;

                    // lower water tiles (temp test)
                    if (newTileType == "water") {
                        newTile.transform.position -= Vector3.up * 0.5f;
                    }

                    // create random array of tiles for trees (not allowing for any water tiles)
                    if (newTileType != "water") {
                        allGroundTilePositions.Add(tilePosition);
                        allGroundTilePositionsShuffled = new Queue<Vector3>(Utility.ShuffleArray(allGroundTilePositions.ToArray(), 10));
                    }
                }
            }
        }
                

        // add trees
        // shuffle list of all ground tiles to randomly select some for trees
        

        for (int i = 0; i < treeCount; i++) {
            Vector3 randomPosition = GetRandomPosition();
            Transform newTree = Instantiate(treePrefab, randomPosition, Quaternion.identity);
            newTree.parent = mapHolder;
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

        width = Mathf.Sqrt(3) * tileSize;
        height = 2f * tileSize * (3f/4f);

        mapOffsetx = width * mapSize.x/2;
        mapOffsety = height * mapSize.y/2;

        offset = (y % 2 == 0) ? width / 2 : 0;

        return new Vector3((x * width) + offset - mapOffsetx, 0, (y * height) - mapOffsety);
   
    }

    public Vector3 GetRandomPosition() {
        Vector3 randomPosition = allGroundTilePositionsShuffled.Dequeue();
        allGroundTilePositionsShuffled.Enqueue(randomPosition);
        return randomPosition;
    }
}










                



    
       

        

                
        
                










