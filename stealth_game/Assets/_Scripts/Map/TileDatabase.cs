using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDatabase : ScriptableObject {

    public enum TileType {
        Dirt,
        Grass,
        Water
    }

    public GameObject Dirt;
    public GameObject Grass;
    public GameObject Water;

    public GameObject GetTile(TileType tileType) {
    
            switch (tileType) {
                
                case TileType.Dirt:
                    return Dirt;
                 case TileType.Grass:
                    return Grass;
                case TileType.Water:
                    return Water;

        }
        return null;
    }

}

    

