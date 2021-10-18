using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public long seed = 10;
    public GameObject chunkPrefab;

    private WorldGenerator _worldGenerator;
    private readonly Dictionary<Vector2Int, ChunkController> _chunkMap = new Dictionary<Vector2Int, ChunkController>();


    private void Awake()
    {
        _worldGenerator = new WorldGenerator(seed);
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
        // TODO deserialization
        _worldGenerator.GenerateChunk(n);
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
