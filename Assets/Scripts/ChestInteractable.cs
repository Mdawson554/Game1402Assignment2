using UnityEditor;
using DG.Tweening;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator anim;
    
    private int isOpenHash;
    private Tween _loopTween;
    private Tween _collectTween;
    
    public Shooter shooter;

    void Start()
    {
        if(!anim) return;
        isOpenHash = Animator.StringToHash("IsOpen");
    }

    public void OnHoverIn()
    {
        anim?.SetBool(isOpenHash, true);
        Toast.Instance.ShowToast("Press \"E\" to Interact");
    }

    public void OnHoverOff()
    {
        anim?.SetBool(isOpenHash, false);
        Toast.Instance.HideToast();
    }

    public void OnInteract()
    {
        GameManager.Instance.AddArrowsToInventory();
        _collectTween = transform.DOScale(0, .5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void OnDestroy()
    {
        DOTween.Kill(this.gameObject);
    }
    
    
}
