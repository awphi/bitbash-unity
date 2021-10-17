using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkController : MonoBehaviour
{
    private Rect _boundingRect = Rect.zero;
    public Rect BoundingRect
    {
        get => _boundingRect;
        private set => _boundingRect = value;
    }

    private Vector2Int _coords = Vector2Int.zero;
    public Vector2Int Coords
    {
        get => _coords;
        private set => _coords = value;
    }

    private readonly Dictionary<int, TilemapController> _cache = new Dictionary<int, TilemapController>();

    public TilemapController GetLayer(int n)
    {
        if(_cache.ContainsKey(n)) {
            return _cache[n];
        }

        var c = transform.Find("Layer " + n);

        if(c == null)
        {
            // TODO in the future add programmatically more layers here if needed
            throw new Exception("Invalid layer given: " + n);
        }

        _cache[n] = c.GetComponent<TilemapController>();
        return _cache[n];
    }

    public void Init(Vector2Int loc)
    {
        Coords = loc;
        transform.position = new Vector3(Coords.x * CoordinateUtils.ChunkSize,
            Coords.y * CoordinateUtils.ChunkSize, 0f);
        BoundingRect = new Rect(transform.position, new Vector2Int(CoordinateUtils.ChunkSize, CoordinateUtils.ChunkSize));
    }

    public void SetTileAt(int layer, int x, int y, LevelTile tile)
    {
        GetLayer(layer).SetTileAt(x, y, tile);
    }

    public TileBase GetTileAt(int layer, int x, int y)
    {
        return GetLayer(layer).GetTileAt(x, y);
    }
}
