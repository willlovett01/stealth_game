using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoordinateConversions {

    // change from offset coordinates to cube coordinates
    public static Vector3Int OffsetToCube(int offset_x, int offset_y) {
        var q = offset_x - (offset_y + (offset_y % 2)) / 2;
        var r = offset_y;
        return new Vector3Int(q, r, -q - r);
    }

    //change from cube coordinates to offset coordinates
    public static Vector2Int CubeToOffset(int cube_x, int cube_y) {
        int col = cube_x + (cube_y + (cube_y & 1)) / 2;
        int row = cube_y;
        return new Vector2Int(col, row);
    }



    // generate tile position based on its coordinate
    public static Vector3 CoordToPosition(int x, int y, float radius, float tileSize) {
        float height = radius;
        float width = radius;

        width = Mathf.Sqrt(3) * tileSize;
        height = 2f * tileSize * (3f / 4f);

        //float mapOffsetx = width * mapSize.x / 2;
        //float mapOffsety = height * mapSize.y / 2;

        float offset = (y % 2 == 0) ? width / 2 : 0;

        // unsure what offset numbers mean (3.46 and 2.59) but they work with the current tiles at any radius
        return new Vector3((x * width) + offset - 3.46f - (2.59f * (radius - 1f)), 0, (y * height) - (radius * 1.5f));

    }

    // get neighbours 
    public static List<TilePiece> GetNeighbours(TilePiece tile, List<TilePiece> allTiles) {

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
