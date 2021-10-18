using Unity.Mathematics;
using UnityEngine;

public static class CoordinateUtils
{
    public const int ChunkSize = 8;

    public static Vector2Int GetChunkAtWorldPosition(Vector3 loc)
    {
        return new Vector2Int((int) math.floor(loc.x / ChunkSize), (int) math.floor(loc.y / ChunkSize));
    }
    
    public static Vector2Int GetRawTilePosition(Vector2Int chunk, int i, int j)
    {
        return new Vector2Int(chunk.x * ChunkSize + i, chunk.y * ChunkSize + j);
    }

    public static Vector2Int GetRawTilePosition(Vector2Int chunk, Vector2Int pos)
    {
        return GetRawTilePosition(chunk, pos.x, pos.y);
    }
}
