using System;
using System.Collections.Generic;
using UnityEngine;

class Rooms
{
    [SerializeField] public Vector2Int position;
    public Vector2Int size;
    public List<Vector2Int> tiles = new List<Vector2Int>();

    public enum Type
    {
        Breaker,
        Breakroom,
        Office,
        Corridor,
        Factory
    }
    public Type roomType;

    public Rooms(Vectort2Int pos, Vector2Int sz, Type type)
    {
        position = pos;
        size = sz;
        roomType = type;
        tiles = new list<Vector2Int>();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                tiles.Add(new Vector2Int(position.x + x, position.y + y,));
            }
        }
    }
}   
