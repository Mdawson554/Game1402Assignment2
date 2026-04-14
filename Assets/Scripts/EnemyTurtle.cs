using DG.Tweening;
using UnityEngine;

public class EnemyTurtle : EnemyBehaviour
{
    [SerializeField] private AudioClip turtleDeath;
    private AudioManager audioManager;
    
    public override void EnemyDeath()
    {
        audioManager.PlaySound(turtleDeath);
        Debug.Log("Turtle Dies");
        DOTween.Kill(this.gameObject);
    }
}
