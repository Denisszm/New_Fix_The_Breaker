using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Corridor
{
    public Vector2Int start;
    public Vector2Int end;
    public List<Vector2Int> tiles = new List<Vector2Int>();
    public Color color = Color.gray;

    public Corridor(Vector2Int from, Vector2Int to)
    {
        start = from;
        end = to;
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        tiles = new List<Vector2Int>();
        int xDir;
        if (start.x < end.x)
        {
            xDir = 1;
        }
        else
        {
            xDir = -1;
        }
        int yDir;
        if (start.x < end.x)
        {
            yDir = 1;
        }
        else
        {
            yDir = -1;
        }

        for (int x = start.x; x != end.x; x += xDir)
        {
            tiles.Add(new Vector2Int(x, start.y));
        }
        for (int y = start.y; y != end.y; y += yDir)
        {
            tiles.Add(new Vector2Int(end.x, y));
        }
        tiles.Add(end);
    }

    public bool IsColliding(Room room)
    {
        foreach (Vector2Int t in tiles)
        {
            foreach (Vector2Int rt in room.tiles)
            {
                if (t == rt)
                    return true;
            }
        }
        return false;
    }
}
