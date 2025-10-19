using UnityEngine;

public class Olaf_Player_Movement : MonoBehaviour
{
    public float Speed = 8f;
    private Vector2 moveDirection;

    public Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        float MovementX = Input.GetAxisRaw("Horizontal");
        float MovementY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(MovementX, MovementY).normalized;

        if (rb.position.y < -2 && rb.position.y > -3)
        {
            transform.localScale = new Vector3(rb.position.y * 0.7f, rb.position.y * 0.7f, 1);
        }
        else if(rb.position.y >= -2)
        {
            transform.localScale = new Vector3(-1.5f* 1, -1.5f * 1, 1);
        }
        else if (rb.position.y <= -3)
        {
            transform.localScale = new Vector3(-2.3f, -2.3f, 1);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * Speed, moveDirection.y * Speed);
    }
}
