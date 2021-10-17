using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New LevelTile", menuName = "2D/LevelTile")]
public class LevelTile : Tile
{
    public TileType type;
}

[Serializable]
public enum TileType
{
    Grass = 0,
    Sand = 1,
    Water = 2,
    Stone = 3
}