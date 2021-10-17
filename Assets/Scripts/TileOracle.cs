using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileOracle : MonoBehaviour
{
    public GameObject palette;
    
    private static readonly Dictionary<TileType, LevelTile> _map = new Dictionary<TileType, LevelTile>();

    private void Awake()
    {
        var map = palette.GetComponentInChildren<Tilemap>();
        var allTiles = map.GetTilesBlock(map.cellBounds);
        foreach(var tile in allTiles)
        {
            if (!(tile is LevelTile lt))
            {
                Debug.LogError("Non-LevelTile tile exists in the tile oracle's palette! " + tile);
                continue;
            }

            _map.Add(lt.type, lt);
        }
        
        Debug.Log("TileOracle registered " + _map.Keys.Count + " LevelTiles!");
    }

    public static LevelTile GetTile(TileType id)
    {
        return _map[id];
    }
}