using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TileData
{
    Tile baseTile;
}

public class TilemapRandomizer : MonoBehaviour
{
    public Tilemap tilemap;
    public TileData tile;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = transform.GetComponentInChildren<Tilemap>();
    }

    

    public void RandomizeTiles()
    {
        
    }


}
