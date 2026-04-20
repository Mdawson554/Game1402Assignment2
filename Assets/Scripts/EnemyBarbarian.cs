using DG.Tweening;
using UnityEngine;

public class EnemyBarbarian : EnemyBehaviour
{
    [SerializeField] private AudioClip barbarianDeath;
    private AudioManager audioManager;

    public override void EnemyDeath()
    {
        audioManager = AudioManager.Instance;
        if (barbarianDeath == null || audioManager == null) return;
        Debug.Log("Barbarian Dies");
        audioManager.PlaySound(barbarianDeath);
        transform.DOScale(0, .5f).SetEase(Ease.InBack).OnComplete(() => { Destroy(gameObject); });
    }
}