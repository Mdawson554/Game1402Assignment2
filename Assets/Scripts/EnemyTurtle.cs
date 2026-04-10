using UnityEngine;

public class EnemyTurtle : EnemyBehaviour
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override bool CanDetectPlayer()
    {
        bool detected = enemyLineOfSight.IsDetected && enemySenses.IsInFOV();
        Debug.Log("Turtle can detect player: " + detected);
        return detected;
    }
}
