using UnityEngine;
using DG.Tweening;

public class ButterfliesInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private int butterflyAmount = 1;
    private Tween _collectTween;
 
    public void OnHoverIn()
    {
        Debug.Log("Interact in!");
        Toast.Instance.ShowToast("Press \"E\" to Interact");
    }

    public void OnHoverOff()
    {
        Debug.Log("Interact out!");
        Toast.Instance.HideToast();
    }
    
    public void OnInteract()
    {
        InventoryManager.Instance.AddButterfliesToInventory(butterflyAmount);
        Debug.Log($"Interacted with {gameObject.name}");

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
