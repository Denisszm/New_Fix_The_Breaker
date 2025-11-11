using UnityEngine;

public class CrawlerScript : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float movementDetection;

    private float distance;

    public Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < movementDetection)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            animator.SetBool("isWalk", true);
            if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
                spriteRenderer.flipX = true;
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
            animator.SetBool("isWalk", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "Fake Player")
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
