using UnityEditor;
using DG.Tweening;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator anim;

    private int isOpenHash;
    private Tween _loopTween;
    private Tween _collectTween;

    void Start()
    {
        if(!anim) return;

        isOpenHash = Animator.StringToHash("IsOpen");

        _loopTween = transform.DOScale(1.6f, .2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad).SetDelay(Random.Range(0.5f, 2.5f));
    }

    public void OnHoverIn()
    {
        Debug.Log("Interact in!");
        anim?.SetBool(isOpenHash, true);

        // TODO - Show UI
        Toast.Instance.ShowToast("Press \"E\" to Interact");
    }

    public void OnHoverOff()
    {
        anim?.SetBool(isOpenHash, false);
        Debug.Log("Interact out!");

        // TODO - Hide UI
        Toast.Instance.HideToast();
    }

    public void OnInteract()
    {
        Debug.Log($"Interacted with {gameObject.name}");

        _collectTween = transform.DOScale(0, .5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    void OnDestroy()
    {
        DOTween.Kill(this.gameObject);
    }
}
