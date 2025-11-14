using UnityEngine;

public class Collision : MonoBehaviour
{
    public float speed = 5f;
    private NewRoomGeneration world;

    void Start()
    {
        world = FindObjectOfType<NewRoomGeneration>();
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 desiredMove = input * speed * Time.deltaTime;

        Vector3 newPos = transform.position + desiredMove;

        
        Vector2Int tile = new Vector2Int(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));

        if (IsWalkable(tile))
        {
            transform.position = newPos;
        }
    }

    bool IsWalkable(Vector2Int tile)
    {
        if (world.worldTiles.ContainsKey(tile))
        {
            return world.worldTiles[tile] == Room.TileType.Floor;
        }

        
        return false;
    }


}
