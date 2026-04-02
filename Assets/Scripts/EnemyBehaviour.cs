using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class EnemyBehaviour : MonoBehaviour
{
    private EnemyState _currentState;

    [Header("Ranges")] 
    [SerializeField] protected float patrolRadius;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float giveUpDistance;
    [SerializeField] protected float chaseDistance;
    [SerializeField] private float chaseCheckAngle;

    [Header("Movement")] 
    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected int minIdleTime;
    [SerializeField] protected int maxIdleTime;

    [Header("Enemy Properties")] 
    [SerializeField] protected int damage;
    [SerializeField] protected Transform playerTarget;
    [SerializeField] protected Transform[] patrolTargets;
    [SerializeField] protected Animator enemyAnim;

    protected EnemyLineOfSight enemyLineOfSight;
    
    private Transform currentTarget;
    private NavMeshAgent agent;
    private bool _isWaiting;
    private Vector3 directionToPlayer;
    
    private void Awake()
    {
        enemyLineOfSight = GetComponent<EnemyLineOfSight>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        agent.SetDestination(playerTarget.position);
    }

    void FixedUpdate()
    {
        if (_currentState == EnemyState.IDLE)
        {
            enemyAnim.SetBool("Idle", true);
            
            if (!_isWaiting)
            {
                StartCoroutine(WaitAndChooseARandomPointAndMove(5));
            }

            if (IsPlayerInRange() && IsInFOV())
            {
                _currentState = EnemyState.CHASE;
                enemyAnim.SetBool("Idle", false);
            }
        }

        else if (_currentState == EnemyState.PATROL)
        {
            enemyAnim.SetBool("Walk", true);
            if (agent.remainingDistance <= .2f)
            {
                _currentState = EnemyState.IDLE;
                enemyAnim.SetBool("Walk", false);
            }

            if (IsPlayerInRange() && IsInFOV())
            {
                _currentState = EnemyState.CHASE;
                enemyAnim.SetBool("Walk", false);
            }
        }
        else if (_currentState == EnemyState.CHASE)
        {
            enemyAnim.SetBool("Chase", true);
            agent.SetDestination(playerTarget.position);

            if (IsPlayerEvaded())
            {
                _currentState = EnemyState.IDLE;
                enemyAnim.SetBool("Chase", false);
            }
        }
    }
    
    protected virtual IEnumerator WaitAndChooseARandomPointAndMove(int idleTime)
    {
        _isWaiting = true;
        Debug.Log("Waiting to choose a random point");
        yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
        _currentState = EnemyState.PATROL;
        enemyAnim.SetBool("Idle", false);
        ChooseARandomPointAndMove();
        _isWaiting = false;
    }

    private void ChooseARandomPointAndMove()
    {
        if (patrolTargets.Length <= 0) return;
        currentTarget = patrolTargets[Random.Range(0, patrolTargets.Length)];
        agent.SetDestination(currentTarget.position);
        enemyAnim.SetBool("Walk", false);
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTarget.position) <= chaseDistance;
    }

    private bool IsPlayerEvaded()
    {
        return Vector3.Distance(transform.position, playerTarget.position) <= giveUpDistance;
    }
    
    private bool IsInFOV()
    {
        directionToPlayer = (playerTarget.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, directionToPlayer) <= chaseCheckAngle;
    }
}