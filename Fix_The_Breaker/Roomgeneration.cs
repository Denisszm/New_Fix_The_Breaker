using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [Header("World Settings")]
    [SerializeField] private int roomCount = 40;
    [SerializeField] private Vector2Int worldSize = new Vector2Int(100, 100);

    [Header("Room Size Settings")]
    [SerializeField] private Vector2Int minRoomSize = new Vector2Int(4, 4);
    [SerializeField] private Vector2Int maxRoomSize = new Vector2Int(10, 10);

    [Header("Biome Split (0.5 = Jämt)")]
    [Range(0f, 1f)]
    [SerializeField] private float biomeSplit = 0.5f;

    private List<Room> rooms = new List<Room>();
    private List<Corridor> corridors = new List<Corridor>();

    void Start()
    {
        GenerateRooms();
        GenerateCorridors();
    }

    void GenerateRooms()
    {
        rooms.Clear();
        int attempts = 0;

        while (rooms.Count < roomCount && attempts < roomCount * 20)
        {
            attempts++;

            Vector2Int pos = new Vector2Int(
                    Unitiy.Random.Range(-worldSize.x / 2, worldSize.x / 2),
                    Unity.Random.Range(-worldSize.y / 2, worldSize.y / 2)
                );

            Vector2Int size = new Vector2Int(
                    Unity.Random.Range(minRoomSize.x, maxRoomSize.x),
                    Unity.Random.Range(minRoomSize.y, maxRoomSize.y
                );
            

            

        }
    }

    void GenerateCorridors()
    {
        corridors.Clear();

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            Room a = rooms[i];
            Room b = rooms[i + 1];

            Corridor c = new Corridor(a.Center(), b.Center());
            corridors.Add(c);
        }
    }

    void OnDrawGizmos()
    {
        if (rooms != null)
        {
            foreach (Room r in rooms)
            {
                Gizmos.color = r.color;
                Vector3 center = new Vector3(r.position.x + r.size.x / 2f,
                                             r.position.y + r.size.y / 2f,
                                             0);
                Vector3 size = new Vector3(r.size.x, r.size.y, 0.1f);
                Gizmos.DrawCube(center, size);
            }
        }

        if (corridors != null)
        {
            Gizmos.color = Color.gray;
            foreach (Corridor c in corridors)
            {
                foreach (Vector2Int t in c.tiles)
                {
                    Vector3 tileCenter = new Vector3(t.x + 0.5f, t.y + 0.5f, 0);
                    Vector3 tileSize = new Vector3(1, 1, 0.1f);
                    Gizmos.DrawCube(tileCenter, tileSize);
                }
            }
        }
    }
}
