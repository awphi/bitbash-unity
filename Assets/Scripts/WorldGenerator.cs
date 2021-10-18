using Unity.Mathematics;
using UnityEngine;

public class WorldGenerator
{
    private const double HEIGHT_SHIFT = 0.53;
    private const double MOISTURE_SIZE = 120.0;
    private const double HEIGHT_SIZE = 80.0;
    
    private readonly OpenSimplexNoise _heightNoise;
    private readonly OpenSimplexNoise _moistureNoise;
    
    public WorldGenerator(long seed)
    {
        _heightNoise = new OpenSimplexNoise(seed);
        _moistureNoise = new OpenSimplexNoise(seed + 16);
    }

    private int GetHeightAt(Vector2Int pos)
    {
        // Octaves, 1 + 0.5 + 0.25 = 1.75
        var e = 1 * _heightNoise.Evaluate(pos.x / HEIGHT_SIZE, pos.y / HEIGHT_SIZE)
                          + 0.5 * _heightNoise.Evaluate(2 * (pos.x / HEIGHT_SIZE), 2 * (pos.y / HEIGHT_SIZE))
                          + 0.25 * _heightNoise.Evaluate(4 * (pos.x / HEIGHT_SIZE), 4 * (pos.y / HEIGHT_SIZE));
        
        // e / sum of octaves then bound between 0 and 1
        var norm = (e / 1.75 + 1) / 2;
        return (int) math.round(math.pow(norm, HEIGHT_SHIFT) * 100);
    }
    
    private TileType GenerateTileTypeAt(Vector2Int pos)
    {
        var height = GetHeightAt(pos);
        // TODO moisture
        
        return height > 70 ? TileType.Grass : TileType.Sand;
    }

    public void GenerateChunk(ChunkController chunk)
    {
        for (var i = 0; i < CoordinateUtils.ChunkSize; i++)
        {
            for (var j = 0; j < CoordinateUtils.ChunkSize; j++)
            {
                var tile = TileOracle.GetTile(GenerateTileTypeAt(CoordinateUtils.GetRawTilePosition(chunk.Coords, i, j)));
                chunk.SetTileAt(0, i, j, tile);
            }
        }
    }
}
