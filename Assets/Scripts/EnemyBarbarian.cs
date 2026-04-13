using DG.Tweening;
using UnityEngine;

public class EnemyBarbarian : EnemyBehaviour
{
    [SerializeField] private AudioClip BarbarianDeath;
    private AudioManager audioManager;
    
    public override void EnemyDeath()
    {
       audioManager.PlaySound(BarbarianDeath);
        transform.DOScale(0, .5f).SetEase(Ease.InBack).OnComplete(() => { Destroy(gameObject); });
        Debug.Log("Barbarian Dies");
    }
}
