using System.Collections.Generic;
using Unity.IntegerTime;
using UnityEngine;

public class NewRoomGeneration : MonoBehaviour
{
    [Header("World Settings")]
    [SerializeField] private int roomCount = 40;
    [SerializeField] private Vector2Int worldSize = new Vector2Int(100, 100);

    [Header("Room Size Settings")]
    [SerializeField] private Vector2Int minRoomSize = new Vector2Int(4, 4);
    [SerializeField] private Vector2Int maxRoomSize = new Vector2Int(10, 10);

    [Header("Corridor Size Settings")]
    [SerializeField] private int Thickness = 2;

    [Header("Biome Split (0.5 = Jämt)")]
    [Range(0f, 1f)]
    [SerializeField] private float biomeSplit = 0.5f;

    [Header("Floor To Wall Ratio (0.5 = Jämt)")]
    [Range(0f, 1f)]
    [SerializeField] private float FloorToWall = 0.5f;

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

        AddSpecialRoom(Room.Type.Breakroom, -worldSize.x / 4);
        AddSpecialRoom(Room.Type.Breakroom, worldSize.x / 4);

        AddSpecialRoom(Room.Type.Breaker, worldSize.x / 2);

        while (rooms.Count < roomCount && attempts < roomCount * 20)
        {
            attempts++;

            Vector2Int pos = new Vector2Int(
                    UnityEngine.Random.Range(-worldSize.x / 2, worldSize.x / 2),
                    UnityEngine.Random.Range(-worldSize.y / 2, worldSize.y / 2)
                );
            Vector2Int size = new Vector2Int(
                    UnityEngine.Random.Range(minRoomSize.x, maxRoomSize.x),
                    UnityEngine.Random.Range(minRoomSize.y, maxRoomSize.y)
                );

            Room.Type type;
            if (pos.x < worldSize.x * (biomeSplit - 0.5f))
            {
                type = Room.Type.Factory;
            }
            else
            {
                type = Room.Type.Office;
            }

            Room newRoom = new Room(pos, size, type, FloorToWall);
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

    void AddSpecialRoom(Room.Type type, int centerX)
    {
        Vector2Int pos = new Vector2Int(
            centerX + UnityEngine.Random.Range(-10, 10),
            UnityEngine.Random.Range(-worldSize.y / 2, worldSize.y / 2));
        Vector2Int size = new Vector2Int(6, 6);

        Room newRoom = new Room(pos, size, type, FloorToWall);
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

    void GenerateCorridors()
    {
        corridors.Clear();

        HashSet<Room> connectedRooms = new HashSet<Room>();
        connectedRooms.Add(rooms[0]);

        while (connectedRooms.Count < rooms.Count)
        {
            Room closestConnected = null;
            Room closestUnconnected = null;
            float minDistance = float.MaxValue;

            foreach (Room connected in connectedRooms)
            {
                foreach (Room unconnected in rooms)
                {
                    if ( connectedRooms.Contains(unconnected))
                    {
                        continue;
                    }
                    float distance = Vector2Int.Distance(connected.Center(),unconnected.Center());
                    if ( distance < minDistance)
                    {
                        minDistance = distance;
                        closestConnected = connected;
                        closestUnconnected = unconnected;
                    }
                }
            }

            if ( closestConnected != null && closestUnconnected != null)
            {
                Corridor corridor = new Corridor(closestConnected.Center(), closestUnconnected.Center(), rooms, Thickness);
                corridors.Add(corridor);
                connectedRooms.Add(closestUnconnected);
            }
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

public class Room
{
    public Vector2Int position;
    public Vector2Int size;
    public List<Vector2Int> tiles = new List<Vector2Int>();
    public Dictionary<Vector2Int, TileType> tileTypes = new Dictionary<Vector2Int, TileType>();
    public float floorToWallRatio;
    public Color color;

    public enum Type
    {
        Breaker,
        Breakroom,
        Office,
        Factory
    }
    public enum TileType
    {
        Floor,
        Wall
    }
    public Type roomType;

    public Room(Vector2Int pos, Vector2Int sz, Type type, float ratio)
    {
        position = pos;
        size = sz;
        roomType = type;
        floorToWallRatio = ratio;
        color = GetColorByType(type);

        tiles = new List<Vector2Int>();
        tileTypes = new Dictionary<Vector2Int, TileType>();

        int wallHeight = Mathf.RoundToInt(size.y * (1f - floorToWallRatio));

        for ( int x = 0; x < size.x; x++)
        {
            for ( int y = 0; y < size.y; y++)
            {
                Vector2Int tilePos = new Vector2Int(position.x + x, position.y + y);
                tiles.Add(tilePos);

                if ( y >= size.y - wallHeight)
                {
                    tileTypes[tilePos] = TileType.Wall;
                }
                else
                {
                    tileTypes[tilePos] = TileType.Floor;
                }
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

public class Corridor
{
    public Vector2Int start;
    public Vector2Int end;
    public List<Vector2Int> tiles = new List<Vector2Int>();
    public Dictionary<Vector2Int, Room.TileType> tileTypes = new Dictionary<Vector2Int, Room.TileType>();
    public Color color = Color.gray;

    private int thickness;
    private List<Room> roomList = new List<Room>();

    public Corridor(Vector2Int from, Vector2Int to, List<Room> rooms, int thick = 1)
    {
        start = from;
        end = to;
        thickness = thick;
        roomList = rooms;
        GenerateTiles();
    }

    void GenerateTiles()
    {
        tiles = new List<Vector2Int>();
        tileTypes = new Dictionary<Vector2Int, Room.TileType>();
        
        if ( start.x ==  end.x)
        {
            int yDir;
            if ( start.y < end.y)
            {
                yDir = 1;
            }
            else
            {
                yDir = -1;
            }

            for ( int y = start.y; y != end.y; y += yDir)
            {
                tiles.Add(new Vector2Int(start.x, y));
            }
        }
        else if (start.y == end.y)
        {
            int xDir;
            if (start.x < end.x)
            {
                xDir = 1;
            }
            else
            {
                xDir = -1;
            }

            for (int x = start.x; x != end.x; x += xDir)
            {
                tiles.Add(new Vector2Int(x, start.y));
            }
        }
        else
        {
            int xDir;
            if (start.x < end.x)
            {
                xDir = 1;
            }
            else
            {
                xDir = -1;
            }

            for (int x = start.x; x != end.x; x += xDir)
            {
                tiles.Add(new Vector2Int(x, start.y));
            }

            int yDir;
            if (start.y < end.y)
            {
                yDir = 1;
            }
            else
            {
                yDir = -1;
            }

            for (int y = start.y; y != end.y; y += yDir)
            {
                tiles.Add(new Vector2Int(end.x, y));
            }
        }

        tiles.Add(end);
    }
}