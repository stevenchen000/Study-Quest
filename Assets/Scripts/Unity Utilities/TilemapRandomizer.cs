using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[Serializable]
public class ReplacementTile
{
    public TileBase tile;
    [Range(0,1)]
    public float chance;
}

[Serializable]
public class TileData : ISerializationCallbackReceiver
{
    [SerializeField]
    private TileBase baseTile;
    [Range(0,1)]
    [SerializeField]
    private float replacementChance;
    [SerializeField]
    private bool equalizeChances;
    [SerializeField]
    private List<ReplacementTile> replacements = new List<ReplacementTile>();

    public bool IsSameTile(TileBase tile) 
    {
        bool isTile = baseTile == tile;

        if (!isTile)
        {
            for(int i = 0; i < replacements.Count; i++)
            {
                TileBase replaceTile = replacements[i].tile;
                if(replaceTile == tile)
                {
                    isTile = true;
                    break;
                }
            }
        }

        return isTile; 
    }
    public TileBase GetRandomTile()
    {
        TileBase result = null;
        float randInit = UnityEngine.Random.Range(0f, 1f);
        float rand = UnityEngine.Random.Range(0f, 1f);
        float counter = 0;

        if (randInit <= replacementChance)
        {
            for (int i = 0; i < replacements.Count; i++)
            {
                counter += replacements[i].chance;
                if (counter >= rand)
                {
                    result = replacements[i].tile;
                    break;
                }
            }
        }

        if(result == null)
        {
            result = baseTile;
        }

        return result;
    }

    public void OnBeforeSerialize()
    {
        if (equalizeChances)
        {
            equalizeChances = false;
            int numOfReplacements = replacements.Count;

            for(int i = 0; i < numOfReplacements; i++)
            {
                replacements[i].chance = 1f / numOfReplacements;
            }
        }
    }

    public void OnAfterDeserialize()
    {
        
    }
}

[ExecuteInEditMode]
public class TilemapRandomizer : MonoBehaviour
{
    [SerializeField]
    private bool randomizeNow = false;
    [SerializeField]
    private bool randomizeOnLoad = false;
    private Tilemap tilemap;
    public List<TileData> replacementTiles = new List<TileData>();

    // Start is called before the first frame update
    void Start()
    {
        tilemap = transform.GetComponentInChildren<Tilemap>();
        if (randomizeOnLoad) RandomizeTiles();
    }

    private void Update()
    {
        if (Application.isEditor && !Application.isPlaying && randomizeNow)
        {
            randomizeNow = false;
            RandomizeTiles();
        }
    }

    public void RandomizeTiles()
    {
        var bounds = tilemap.cellBounds;
        int minX = bounds.xMin;
        int maxX = bounds.xMax;
        int minY = bounds.yMin;
        int maxY = bounds.yMax;

        for(int x = minX; x <= maxX; x++)
        {
            for(int y = minY; y <= maxY; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase currTile = tilemap.GetTile(position);
                TileBase newTile = null;

                for(int i = 0; i < replacementTiles.Count; i++)
                {
                    TileData replacement = replacementTiles[i];
                    if (replacement.IsSameTile(currTile))
                    {
                        newTile = replacement.GetRandomTile();
                        break;
                    }
                }

                if (newTile != null)
                {
                    tilemap.SetTile(position, newTile);
                }
            }
        }
    }


}
