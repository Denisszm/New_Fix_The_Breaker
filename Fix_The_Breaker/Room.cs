using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public Vector2Int position;
    public Vector2Int size;
    public List<Vector2Int> tiles = new List<Vector2Int>();
    public Color color;

    public enum Type
    {
        Breaker,
        Breakroom,
        Office,
        Factory
    }
    public Type roomType;

    public Room(Vector2Int pos, Vector2Int sz, Type type)
    {
        position = pos;
        size = sz;
        roomType = type;
        color = GetColorByType(type);

        tiles = new List<Vector2Int>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                tiles.Add(new Vector2Int(position.x + x, position.y + y));
            }
        }
    }

    public bool IsColliding(Room other)
    {
        return !(position.x + size.x <= other.position.x ||
                 other.position.x + other.size.x <= position.x ||
                 position.y + size.y <= other.position.y ||
                 other.position.y + other.size.y <= position.y);
    }

    private Color GetColorByType(Type type)
    {
        switch (type)
        {
            case Type.Breaker: return Color.red;
            case Type.Breakroom: return Color.green;
            case Type.Office: return Color.blue;
            case Type.Factory: return Color.yellow;
            default: return Color.white;
        }
    }

    public Vector2Int Center()
    {
        return new Vector2Int(position.x + size.x / 2, position.y + size.y / 2);
    }
}
