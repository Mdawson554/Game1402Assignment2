using DG.Tweening;
using UnityEngine;

public class EnemyTurtle : EnemyBehaviour
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void EnemyDeath()
    {
        Debug.Log("Turtle Dies");
        Destroy(gameObject);
    }
}
