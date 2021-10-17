using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public long seed = 10;
    public GameObject chunkPrefab;

    private LevelTile _grassTest;

    private OpenSimplexNoise _heightNoise;
    private OpenSimplexNoise _moistureNoise;
    private readonly Dictionary<Vector2Int, ChunkController> _chunkMap = new Dictionary<Vector2Int, ChunkController>();


    private void Start()
    {
        _heightNoise = new OpenSimplexNoise(seed);
        _moistureNoise = new OpenSimplexNoise(seed + 16);
        _grassTest = TileOracle.GetTile(TileType.Grass);
    }

    public void UnloadChunk(Vector2Int chunk)
    {
        if (!_chunkMap.ContainsKey(chunk)) return;
        
        // TODO serialization
        Destroy(_chunkMap[chunk]);
        _chunkMap.Remove(chunk);
    }

    public ChunkController LoadChunk(Vector2Int chunk)
    {
        if (_chunkMap.ContainsKey(chunk))
        {
            return _chunkMap[chunk];
        }

        var n = CreateChunk(chunk);
        n.SetTileAt(0, 0, 0, _grassTest);
        n.SetTileAt(0, 7, 7, _grassTest);
        n.SetTileAt(0, 0, 7, _grassTest);
        n.SetTileAt(0, 7, 0, _grassTest);
        return n;
    }
    

    private ChunkController CreateChunk(Vector2Int chunk)
    {
        var genChunk = Instantiate(chunkPrefab, transform).GetComponent<ChunkController>();
        genChunk.Init(chunk);
        genChunk.gameObject.name = "Chunk" + chunk;
        _chunkMap[chunk] = genChunk;
        return genChunk;
    }
}
