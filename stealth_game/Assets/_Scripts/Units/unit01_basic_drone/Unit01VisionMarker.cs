using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Unit01VisionMarker : MonoBehaviour {

    public List<TilePiece> allTiles;
    Vector3Int[] radialTilePositions;
    MapGeneratorHex map;
    public Transform visionPrefab;
  

    // Start is called before the first frame update
    void Start() {
        allTiles = GameObject.Find("MapHex").GetComponent<MapGeneratorHex>().allTiles;
        map = GameObject.Find("MapHex").GetComponent<MapGeneratorHex>();
        
        radialTilePositions = new Vector3Int[] {
            
            new Vector3Int(-1, 2,-1),
            new Vector3Int(0, 2, -2),
            new Vector3Int(1, 1, -2),
            new Vector3Int(2, 0, -2),
            new Vector3Int(2, -1, -1),
            new Vector3Int(2, -2, 0),
            new Vector3Int(1, -2, 1),
            new Vector3Int(0, -2, 2),
            new Vector3Int(-1,-1 ,2),
            new Vector3Int(-2, 0, 2),
            new Vector3Int(-2, 1, 1),
            new Vector3Int(-2, 2, 0)

            
        };
    }

    // Update is called once per frame
    void Update() {
            
        transform.position = GetUnitPosition();
        transform.eulerAngles = Vector3.zero;


    }
        
     Vector3 GetUnitPosition() {
        Transform unit = transform.parent.parent;

        TilePiece currentTile = unit.GetComponent<Unit01StateMachine>().currentCoord.GetComponent<TilePiece>();
        TilePiece facingTile = new TilePiece();

        // adding to angle to fix rounding errors
        float unitAngle = unit.transform.rotation.eulerAngles.y + 1f;

        // Convert units facing degrees to an index that can be used to retrieve looking at tile based off tiles cube coords
        int unitFacingIndex = Mathf.FloorToInt((unitAngle - unitAngle % 30) / 30);
       
        //Convert facing index to tile coordinate
        Vector3Int tileIndex = radialTilePositions[unitFacingIndex];

        // Cube coordinate of vision center
        Vector3Int visionCenter = (currentTile.cubeCoordinate + tileIndex);

        // convert vision center coordinate to offset coordinate space
        Vector2Int visionCenterOffset = CoordinateConversions.CubeToOffset(visionCenter.x, visionCenter.y);

        // convert vision center offset coordinates to world position
        Vector3 visionCenterWorld = CoordinateConversions.CoordToPosition(visionCenterOffset.x, visionCenterOffset.y, map.mapRadius, map.tileSize);

        return visionCenterWorld;
    }
}
        



        











        





       
        


