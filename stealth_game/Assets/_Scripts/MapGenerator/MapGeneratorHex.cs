using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class MapGeneratorHex : MonoBehaviour {

    //map pieces 
    public Transform[] tilePrefab;
    public Transform[] treePrefab;
    public int treeCount;
    public int treeSeed;

    // map controls
    public float seed = 10;
    public int mapRadius;
    public float tileSize;


    [Range(1f, 100)]
    public float noiseDetail;

    [Range(0, 1)]
    public float outlinePercent;

    public List<TilePiece> allTiles;
    List<TilePiece> allGroundTiles;
    Queue<TilePiece> allGroundTilePositionsShuffled;


    List<Vector2Int> tileCoordinates;

    void Awake() {

        GenerateMap();
    }

    // map generator
    public void GenerateMap() {


        allGroundTiles = new List<TilePiece>();
        allTiles = new List<TilePiece>();
        tileCoordinates = new List<Vector2Int>();

        // map radius coords
        List<Vector3> radialCoordinates = new List<Vector3>();
        List<int> q = new List<int>();
        List<int> r = new List<int>();
        List<int> s = new List<int>();

        // create parent object for map
        string holderName = "Generated Map";
        if (GameObject.Find(holderName)) {
            DestroyImmediate(GameObject.Find(holderName));
        }
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        // add positive numbers based off radius
        for (int i = 1; i <= mapRadius; i++) {
            q.Add(i);
            r.Add(i);
            s.Add(i);
        }

        // add positive numbers based off radius
        for (int i = 0; i >= -mapRadius; i--) {
            q.Add(i);
            r.Add(i);
            s.Add(i);
        }


        // calculate cube coords for all tiles within desired radius and add them to a list of coords in offset 
        foreach (int _q in q) {
            foreach(int _r in r) {
                foreach (int _s in s) {
                    if (_q + _r + _s == 0) {

                        // add map radius to offset to remove any negative value coords 
                        tileCoordinates.Add((CoordinateConversions.CubeToOffset(_q + mapRadius, _r + mapRadius)));
                    }
                }
            }
        } 
            
        // instantiate tile per coordinate
        foreach (Vector2Int pos in tileCoordinates) {

            Vector3 tilePosition = CoordinateConversions.CoordToPosition(pos.x, pos.y, mapRadius, tileSize);

            // instantiate tile
            Transform newTile = Instantiate(tilePrefab[GenerateNoise(pos.x, pos.y, noiseDetail)], tilePosition, Quaternion.identity) as Transform;
            allTiles.Add(newTile.gameObject.GetComponent<TilePiece>());

            // get tile type (grass, water, etc..)
            string newTileType = newTile.gameObject.GetComponent<TilePiece>().tileType;

            // scale each piece to add outlines for visual clarity
            newTile.localScale *= tileSize;
            newTile.localScale *= (1 - outlinePercent);
            newTile.parent = mapHolder;
            newTile.name = ($"Hex C{pos.x}, R{pos.y}");

            // add random rotation to each tile
            int angle = Random.Range(0, 360);
            int angleIncriments = angle - (angle % 60);
            newTile.Rotate(new Vector3(0, angleIncriments, 0), Space.Self);

            // assign coordinates
            newTile.gameObject.GetComponent<TilePiece>().cubeCoordinate = CoordinateConversions.OffsetToCube(pos.x, pos.y);
            newTile.gameObject.GetComponent<TilePiece>().offsetCoordinate = new Vector2Int(pos.x, pos.y);

            // lower water tiles (temp test)
            if (newTileType == "water") {
                newTile.transform.position -= Vector3.up * 0.5f;
                newTile.gameObject.GetComponent<TilePiece>().IsClickable();
            }

            // create random array of tiles for trees (not allowing for any water tiles)
            if (newTileType != "water") {
                allGroundTiles.Add(newTile.gameObject.GetComponent<TilePiece>());
                allGroundTilePositionsShuffled = new Queue<TilePiece>(Utility.ShuffleArray(allGroundTiles.ToArray(), treeSeed));
            }
        }
                
        // add trees
        // shuffle list of all ground tiles to randomly select some for trees
        for (int i = 0; i < treeCount; i++) {
            TilePiece randomTile = GetRandomTile();
            randomTile.GetComponent<TilePiece>().tileType = "tree";
            Transform treePosition = randomTile.gameObject.transform;

            // make tree tiles non clickable
            treePosition.gameObject.GetComponent<TilePiece>().IsClickable();

            // instantiate tree
            Transform newTree = Instantiate(treePrefab[Random.Range(0,treePrefab.Length)], treePosition.position, Quaternion.identity);
            newTree.Rotate(new Vector3(0, Random.Range(0, 360), 0), Space.Self);
            newTree.parent = mapHolder;
        }

        // add neigbours 
        foreach (TilePiece tile in allTiles) {
            tile.neighbours = GetNeighbours(tile, allTiles);
        }
    }

    // generate noise
    int GenerateNoise(int x, int z, float detailScale) {

        float xNoise = (x + this.transform.position.x + seed) / detailScale;
        float zNoise = (z + this.transform.position.y + seed) / detailScale;

        return (int)(Mathf.PerlinNoise(xNoise, zNoise)*tilePrefab.Length);
    }

    // get a random tile
    public TilePiece GetRandomTile() {
        TilePiece randomTile = allGroundTilePositionsShuffled.Dequeue();
        allGroundTilePositionsShuffled.Enqueue(randomTile);
        return randomTile;
    }

    // get neighbours 
    public List<TilePiece> GetNeighbours(TilePiece tile, List<TilePiece> allTiles) {

        List<TilePiece> neighbours = new List<TilePiece>();

        Vector3Int[] neighbourCoords = new Vector3Int[] {
            new Vector3Int(1,-1,0),
            new Vector3Int(1,0,-1),
            new Vector3Int(0,1,-1),
            new Vector3Int(-1,1,0),
            new Vector3Int(-1,0,1),
            new Vector3Int(0,-1,1)
        };

        foreach (Vector3Int coord in neighbourCoords) {
            foreach (TilePiece piece in allTiles) {
                if (piece.clickable) {
                    Vector3Int tileCoord = piece.cubeCoordinate;


                    if (tileCoord == tile.cubeCoordinate + coord) {
                        neighbours.Add(piece);
                    }
                }
            }
        }
        return neighbours;
    }

}





 














        










                



    
       

        

                
        
                










