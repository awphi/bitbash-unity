using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    private Tilemap _tilemap;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public void SetTileAt(int x, int y, LevelTile tile)
    {
        _tilemap.SetTile(new Vector3Int(x, y, 0), tile);
    }

    public LevelTile GetTileAt(int x, int y)
    {
        return (LevelTile) _tilemap.GetTile(new Vector3Int(x, y, 0));
    }
}