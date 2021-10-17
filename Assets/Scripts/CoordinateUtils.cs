using Unity.Mathematics;
using UnityEngine;

public static class CoordinateUtils
{
    public const int ChunkSize = 8;

    public static Vector2Int GetChunkAtWorldPosition(Vector3 loc)
    {
        return new Vector2Int((int) math.floor(loc.x / ChunkSize), (int) math.floor(loc.y / ChunkSize));
    }
}
