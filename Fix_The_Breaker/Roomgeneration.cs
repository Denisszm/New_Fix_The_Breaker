using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [Header("Room Settings")]
    [SerializeField] private int roomCount = 20;
    [SerializeField] private Vector2Int minRoomSize = new Vector2Int(4, 4);
    [SerializeField] private Vector2Int maxRoomSize = new Vector2Int(10, 10);
    [SerializeField] private int worldSize = 50;

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
                Random.Range(-worldSize, worldSize),
                Random.Range(-worldSize, worldSize)
            );
            Vector2Int size = new Vector2Int(
                Random.Range(minRoomSize.x, maxRoomSize.x),
                Random.Range(minRoomSize.y, maxRoomSize.y)
            );

            Room.Type type = (Room.Type)Random.Range(0, System.Enum.GetValues(typeof(Room.Type)).Length);

            Room newRoom = new Room(pos, size, type);

            bool collides = false;
            foreach (Room r in rooms)
            {
                if (newRoom.IsColliding(r))
                {
                    collides = true;
                    break;
                }
            }

            if (!collides)
            {
                rooms.Add(newRoom);
            }
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
