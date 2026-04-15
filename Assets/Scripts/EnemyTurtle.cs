using DG.Tweening;
using UnityEngine;

public class EnemyTurtle : EnemyBehaviour
{
    [SerializeField] private AudioClip turtleDeath;
    private AudioManager audioManager;
    
    public override void EnemyDeath()
    {
        audioManager = AudioManager.Instance;
        if (audioManager == null || turtleDeath == null) return;
        audioManager.PlaySound(turtleDeath);
        Debug.Log("Turtle Dies");
        Destroy(this.gameObject);
        DOTween.Kill(this.gameObject);
    }
}
