using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerBody;
    public float speed;

    private float distance;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, playerBody.position);
        Vector2 direction = playerBody.position - transform.position;
        direction.Normalize();


        if(distance < 10)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerBody.position, speed * Time.deltaTime);
        }

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
            spriteRenderer.flipX = false;
    }
}
