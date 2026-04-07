using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    private EnemyState _currentState = EnemyState.IDLE;
    
    [Header("Ranges")] 
    [SerializeField] protected float attackRange;
    
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

    private EnemyLineOfSight enemyLineOfSight;
    private EnemySenses enemySenses;
    private Transform currentTarget;
    private NavMeshAgent agent;
    private bool _isWaiting;
    
    private void Awake()
    {
        enemySenses = GetComponent<EnemySenses>();
        enemyLineOfSight = GetComponent<EnemyLineOfSight>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
    }

    private void FixedUpdate()
    {
        if (_currentState == EnemyState.IDLE)
        {
            enemyAnim.SetBool("Idle", true);
            
            if (!_isWaiting)
            {
                StartCoroutine(WaitAndChooseARandomPointAndMove());
            }
            
            if (enemySenses.IsPlayerInRange() && enemySenses.IsInFOV() && enemyLineOfSight.IsDetected)
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
            if (enemySenses.IsPlayerInRange() && enemySenses.IsInFOV() && enemyLineOfSight.IsDetected)
            {
                _currentState = EnemyState.CHASE;
                enemyAnim.SetBool("Walk", false);
            }
        }
        else if (_currentState == EnemyState.CHASE)
        {
            enemyAnim.SetBool("Chase", true);
            agent.SetDestination(playerTarget.position);

            if (enemySenses.IsPlayerEvaded())
            {
                _currentState = EnemyState.IDLE;
                enemyAnim.SetBool("Chase", false);
            }
        }
    }
    
    private IEnumerator WaitAndChooseARandomPointAndMove()
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
    }
}
