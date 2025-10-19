using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    #region Public Settings
    [Header("Enemy Settings")]
    public Transform raycast;
    public LayerMask raycastMask;
    public float raycastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion



    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(inRange)
        {
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength, raycastMask);
            RaycastDebugger();
        }

        //When Player is Detected
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            StopAttack();
        }



        //Sprite turn around
        /*Vector2 direction = raycast.position - transform.position;
        direction.Normalize();

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
            spriteRenderer.flipX = false;*/
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
    }

    private void Move()
    {
        Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        timer = intTimer; //Reset timer when player get in attack range
        attackMode = true; // Check if enemy can still attack
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;

    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, Vector3.left * raycastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, Vector3.left * raycastLength, Color.green);
        }
    }
}
