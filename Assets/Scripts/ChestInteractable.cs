using DG.Tweening;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip chestOpen;
    [SerializeField] private AudioClip chestClose;
    [SerializeField] private AudioClip addedToInventory;
    private int isOpenHash;
    private Tween _loopTween;
    private AudioManager audioManager;

    void Start()
    {
        if (!anim) return;
        isOpenHash = Animator.StringToHash("IsOpen");
        audioManager = AudioManager.Instance;
    }

    public void OnHoverIn()
    {
        anim?.SetBool(isOpenHash, true);
        audioManager.PlaySound(chestOpen);
        Toast.Instance.ShowToast("Press \"E\" to Interact");
    }

    public void OnHoverOff()
    {
        anim?.SetBool(isOpenHash, false);
        audioManager.PlaySound(chestClose);
        Toast.Instance.HideToast();
    }

    public void OnInteract()
    {
        GameManager.Instance.AddArrowsToInventory();
        GameManager.Instance.AddHealthPotionsToInventory();
        audioManager.PlaySound(addedToInventory);
        transform.DOScale(0, .5f).SetEase(Ease.InBack).OnComplete(() => { Destroy(gameObject); });
    }

    public void OnDestroy()
    {
        DOTween.Kill(this.gameObject);
    }
}