using UnityEngine;

public class Olaf_Player_Movement : MonoBehaviour
{
    public float Speed = 8f;
    private Vector2 moveDirection;

    public Rigidbody2D rb;

    public Transform mouseObject;

    public GameObject FlashPreFab;
    public Transform FlashSpawnPoint;



    // Update is called once per frame
    void Update()
    {
        float MovementX = Input.GetAxisRaw("Horizontal");
        float MovementY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(MovementX, MovementY).normalized;

        AimGunAtMouse();
        FlashlightLogic();
        /*if (rb.position.y < -2 && rb.position.y > -3)
        {
            transform.localScale = new Vector3(rb.position.y * 0.7f, rb.position.y * 0.7f, 1);
        }
        else if(rb.position.y >= -2)
        {
            transform.localScale = new Vector3(-1.5f* 2, -1.5f * 2, 1);
        }
        else if (rb.position.y <= -3)
        {
            transform.localScale = new Vector3(-2.3f, -2.3f, 1);
        }*/
    }

    private void AimGunAtMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)FlashSpawnPoint.position).normalized;
        FlashSpawnPoint.right = direction;
    }
    public void FlashlightLogic()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AimGunAtMouse();
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * Speed, moveDirection.y * Speed);
    }
}
