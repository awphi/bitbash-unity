using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using Color = UnityEngine.Color;

public class CameraController : MonoBehaviour
{
    public MapController mapController;

    private const float Padding = 3.1f;

    private Camera _mainCamera;
    //private readonly List<ChunkController> _chunksInFrame = new List<ChunkController>();

    private void Start()
    {
        var go = new GameObject();
        go.AddComponent<ChunkController>();
        _mainCamera = GetComponent<Camera>();
    }

    private static Rect GetCameraRect(Camera cam)
    {
        var pt = cam.transform.position;
        var size = new Vector2(cam.orthographicSize * cam.aspect * 2, cam.orthographicSize * 2);
        return new Rect(pt.x - size.x / 2, pt.y - size.y / 2, size.x, size.y);
    }
    
    private void UpdateChunksInFrame()
    {
        var camRect = GetCameraRect(_mainCamera);
        camRect.x -= Padding;
        camRect.y -= Padding;
        camRect.height += Padding * 2;
        camRect.width += Padding * 2;
        
        DebugUtils.DrawRect(camRect, Color.blue);

        var xt = math.floor(camRect.xMax / CoordinateUtils.ChunkSize) - math.ceil(camRect.xMin / 8) + 1;
        var yt = math.floor(camRect.yMax / CoordinateUtils.ChunkSize) - math.ceil(camRect.yMin / 8) + 1;
        

        var botLeft = new Vector3(camRect.xMin, camRect.yMin);
        //Debug.DrawLine(pt, botLeft, Color.cyan);
        var chunkCoord = CoordinateUtils.GetChunkAtWorldPosition(botLeft);


        for (var i = chunkCoord.x; i <= chunkCoord.x + xt; i++)
        {
            for (var j = chunkCoord.y; j <= chunkCoord.y + yt; j++)
            {
                mapController.LoadChunk(new Vector2Int(i, j));
            }
        }
    }

    private void Update()
    {
        UpdateChunksInFrame();
    }
}
