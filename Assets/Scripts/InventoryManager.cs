using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private int maxButterflies;
    [SerializeField] private int maxArrows;
    [SerializeField] private int maxHealthPotions;
    [SerializeField] private int arrowPickupAmount;
    [SerializeField] private int healthPotionPickupAmount;
    [SerializeField] private InputAction usePotionInput;

    public int currentButterflies;
    public int currentArrows;
    public int currentHealthPotions;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        usePotionInput.Enable();
        usePotionInput.performed += UseHealthPotion;
    }

    private void OnDisable()
    {
        usePotionInput.performed -= UseHealthPotion;
    }

    public void AddButterfliesToInventory(int butterflyGain)
    {
        currentButterflies = Mathf.Min(currentButterflies + butterflyGain, maxButterflies);
        UIManager.Instance.UpdateButterflyUI(currentButterflies);
        GameManager.Instance.CheckWinCondition();
    }

    public void AddArrowsToInventory()
    {
        currentArrows = Mathf.Min(currentArrows + arrowPickupAmount, maxArrows);
        UIManager.Instance.UpdateArrowUI(currentArrows);
    }

    public void ShootArrow()
    {
        currentArrows = Mathf.Max(0, currentArrows - 1);
        UIManager.Instance.UpdateArrowUI(currentArrows);
    }

    public void AddHealthPotionsToInventory()
    {
        currentHealthPotions = Mathf.Min(currentHealthPotions + healthPotionPickupAmount, maxHealthPotions);
        UIManager.Instance.UpdateHealthPotionUI(currentHealthPotions);
    }

    private void UseHealthPotion(InputAction.CallbackContext context)
    {
        if (currentHealthPotions <= 0 || GameManager.Instance._isAtMaxHealth) return;
        currentHealthPotions -= 1;
        GameManager.Instance.GainHealth();
        UIManager.Instance
            .UpdateHealthPotionUI(
                currentHealthPotions); 
        Debug.Log($"Player is at {GameManager.Instance.currentHealth}");
    }
}