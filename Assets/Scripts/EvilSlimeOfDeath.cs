using UnityEngine;
using DG.Tweening;
using UnityEngine.PlayerLoop;

public class EvilSlimeOfDeath : EnemyBehaviour
{

    protected override void FixedUpdate()
    {
        agent.speed = runSpeed;
        enemyAnim.SetBool("Chase", true);
        agent.SetDestination(playerTarget.position);

        if (enemySenses.IsPlayerEvaded())
        {
            agent.speed = walkSpeed;
            SetState(EnemyState.IDLE);
        }
    }
}
