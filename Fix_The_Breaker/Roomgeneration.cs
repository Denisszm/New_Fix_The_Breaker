using System;
using System.Collections.Generic;
using UnityEngine;

public class Roomgeneration : MonoBehaviour
{
    [SerializeField] private int roomCount = 5;
    private List<Rooms> rooms = new List<Rooms>();
    public Random random = new Random();

    void start()
    {
        GenerateRooms();
    }
    void GenerateRooms()
    {
        rooms.Clear();
        
        for (int i = 0; i < roomCount; i++)
        {
            Vector2Int randomPos = new Vector2Int(random.Range(-20, 20), random.Range(-20, 20));
            Vector2Int randomSize = new Vector2Int(random.Range(-20, 20), random.Range(-20, 20));
            Room.Type randomType = (Room.Type)Random.Range(0, System.Enum.GetValues(typeof(Room.Type)).Length)
            Room newRoom = new Room(randomPos, randomSize, randomType);
            rooms.Add(newRoom);

            Debug.Log($"Created room #{i + 1}: Type={newRoom.roomType}, Pos={newRoom.position}, Size={newRoom.size}");
        }
        Debug.Log($"Generated {rooms.Count} rooms total!");
    }
}
