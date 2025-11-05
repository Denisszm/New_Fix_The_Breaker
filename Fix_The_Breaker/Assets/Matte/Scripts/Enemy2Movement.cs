using UnityEngine;
using UnityEngine.AI;

public class Enemy2Movement : MonoBehaviour
{
    #region Public Settings
    [Header("Enemy Settings")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector2 walkPoint;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    #endregion

    #region Private Variables
    bool walkPointSet;
    bool alreadyAttacked;
    #endregion

    private void Awake()
    {
        player = GameObject.Find("Plyer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
    }

    private void Patroling()
    {

    }

    private void Chasing()
    {

    }

    private void Attacking()
    {

    }

}
