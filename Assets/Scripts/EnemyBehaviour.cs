using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyLineOfSight))]
[RequireComponent(typeof(EnemySenses))]
[RequireComponent(typeof(EnemyAttack))]
public abstract class EnemyBehaviour : MonoBehaviour
{
    private EnemyState _currentState = EnemyState.IDLE;
    
    [Header("Movement")] 
    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected int minIdleTime;
    [SerializeField] protected int maxIdleTime;

    [Header("Standard Enemy Properties")] 
    [SerializeField] protected Transform playerTarget;
    [SerializeField] protected Transform[] patrolTargets;
    [SerializeField] protected Animator enemyAnim;

    protected EnemyLineOfSight enemyLineOfSight;
    protected EnemySenses enemySenses;
    protected NavMeshAgent agent;

    private Transform currentTarget;
    private bool _isWaiting;
    
    protected virtual void Awake()
    {
        enemySenses = GetComponent<EnemySenses>();
        enemyLineOfSight = GetComponent<EnemyLineOfSight>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
    }
    
    protected virtual void FixedUpdate()
    {
        if (_currentState == EnemyState.IDLE)
        {
            enemyAnim.SetBool("Idle", true);
            
            if (!_isWaiting)
            {
                StartCoroutine(WaitAndChooseARandomPointAndMove());
            }
            
            if (CanDetectPlayer())
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
            if (CanDetectPlayer())
            {
                _currentState = EnemyState.CHASE;
                enemyAnim.SetBool("Walk", false);
            }
        }
        else if (_currentState == EnemyState.CHASE)
        {
            agent.speed = runSpeed;
            enemyAnim.SetBool("Chase", true);
            agent.SetDestination(playerTarget.position);

            if (enemySenses.IsPlayerEvaded())
            {
                agent.speed = walkSpeed;
                _currentState = EnemyState.IDLE;
                enemyAnim.SetBool("Chase", false);
            }
        }
        else if (_currentState == EnemyState.ATTACK)
        {
            agent.SetDestination(playerTarget.position);
        }
    }
    
    public void SetState(EnemyState newState)
    {
        _currentState = newState;
    }
    
    protected virtual IEnumerator WaitAndChooseARandomPointAndMove()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
        _currentState = EnemyState.PATROL;
        enemyAnim.SetBool("Idle", false);
        ChooseARandomPointAndMove();
        _isWaiting = false;
    }
    
    protected virtual bool CanDetectPlayer()
    {
        bool seesDirectly = enemyLineOfSight.IsDetected;
        bool sensesNearby = enemySenses.HasDetectedPlayer || (enemySenses.IsPlayerInRange() && enemySenses.IsInFOV());
        return seesDirectly || sensesNearby;
    }
    
    protected virtual void ChooseARandomPointAndMove()
    {
        if (patrolTargets.Length <= 0) return;
        currentTarget = patrolTargets[Random.Range(0, patrolTargets.Length)];
        agent.SetDestination(currentTarget.position);
    }

    public virtual void EnemyDeath()
    {
        
    }
}