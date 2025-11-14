using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Camera cam;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    public void SnapToRoom(Room room)
    {
        Vector3 center = new Vector3(
            room.position.x + room.size.x / 2f,
            room.position.y + room.size.y / 2f,
            transform.position.z
        );

        transform.position = center;

        float w = room.size.x;
        float h = room.size.y;
        float aspect = cam.aspect;

        cam.orthographicSize = Mathf.Max(h / 2f, (w / 2f) / aspect);
    }
    public void SnapToCorridor(List<Vector2Int> segment, Corridor corridor)
    {
        // Compute the bounding box of this segment
        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int minY = int.MaxValue;
        int maxY = int.MinValue;

        foreach (var tile in segment)
        {
            if (tile.x < minX) minX = tile.x;
            if (tile.x > maxX) maxX = tile.x;
            if (tile.y < minY) minY = tile.y;
            if (tile.y > maxY) maxY = tile.y;
        }

        // Expand by corridor thickness
        minX -= corridor.thickness / 2;
        maxX += corridor.thickness / 2;
        minY -= corridor.thickness / 2;
        maxY += corridor.thickness / 2;

        float width = maxX - minX + 1;
        float height = maxY - minY + 1;

        Vector3 center = new Vector3(
           minX + width / 2f,
           minY + height / 2f,
           transform.position.z
       );

        transform.position = center;

        float aspect = cam.aspect;
        cam.orthographicSize = Mathf.Max(height / 2f, (width / 2f) / aspect);
    }
}
