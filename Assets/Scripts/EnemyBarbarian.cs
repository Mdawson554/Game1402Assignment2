using DG.Tweening;
using UnityEngine;

public class EnemyBarbarian : EnemyBehaviour
{
    [SerializeField] private AudioClip barbarianDeath;
    private AudioManager audioManager;
    
    public override void EnemyDeath()
    {
        if (barbarianDeath == null || audioManager == null ) return;
        audioManager.PlaySound(barbarianDeath);
        transform.DOScale(0, .5f).SetEase(Ease.InBack).OnComplete(() => { Destroy(gameObject); });
        DOTween.Kill(this.gameObject);
        Debug.Log("Barbarian Dies");
    }
}
