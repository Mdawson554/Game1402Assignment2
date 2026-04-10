using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private AudioManager audioManager;
    private UIManager uiManager;
    private PlayerController playerController;
    
    [SerializeField] private string sceneName;
    [SerializeField] private int maxTime;
    [SerializeField] private int maxButterflies;
    [SerializeField] private int maxArrows;
    [SerializeField] private int minHealth = 0; 
    [SerializeField] private int maxHealth = 0;
    [SerializeField] private int startingHealth = 0;
    [SerializeField] private int healthBoostAmount = 0;
    [SerializeField] private int maxHealthPotions;
    
    [SerializeField] private int arrowPickupAmount;
    [SerializeField] private int healthPotionPickupAmount;
   
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private InputAction usePotionInput;
    
    public float currentTime;
    public int currentArrows;
    public int currentHealthPotions;
    public int currentButterflies;
     public int currentHealth;
    private bool _gameOver; 
   
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    
    private void Start()
    {
        audioManager = AudioManager.Instance;
        uiManager = UIManager.Instance;

        ResetHealth();
        currentTime = maxTime;
        ShowMouse(false);
        audioManager.PlayBGMusic();
    }
    
   private void Update()
    {
        DecreaseTimerTime();
    }
   
   
    private void DecreaseTimerTime()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            UIManager.Instance.UpdateTimerUI(currentTime);
            LoseFunction();
            return;
        }
        UIManager.Instance.UpdateTimerUI(currentTime);
    }

    public void CollectButterflies(int butterflyGain)
    {
        currentButterflies += butterflyGain;
        UIManager.Instance.UpdateButterflyUI(currentButterflies);
        WinFunction();
    }
    
    public void AddArrowsToInventory() 
    {
        currentArrows = Mathf.Min(currentArrows + arrowPickupAmount, maxArrows);
        UIManager.Instance.UpdateArrowUI(currentArrows);
    }
    
    public void AddHealthPotionsToInventory() //add a key to use the health potions
    {
        currentHealthPotions = Mathf.Min(currentHealthPotions + healthPotionPickupAmount, maxHealthPotions);
        UIManager.Instance.UpdateHealthPotionUI(currentHealthPotions);
    }

    private void UseHealthPotion(InputAction.CallbackContext context)
    {
        if (currentHealthPotions <= 0) return;
        currentHealthPotions = currentHealthPotions - 1; 
        GainHealth();
    }

    private void GainHealth()
    {
        
        if (currentHealth >= maxHealth) return;  // already full, do nothing
        
        int actualHeal = Mathf.Min(healthBoostAmount, maxHealth - currentHealth);

        currentHealth = currentHealth + actualHeal;
        
        uiManager.IncrementHeartSprite(actualHeal);
        uiManager.UpdateHealthPotionUI(currentHealthPotions);
    }
    
    public void LooseHealth(int damage)
    {
        uiManager.DecrementHeartSprite(damage); 
        uiManager.UpdateHealthPotionUI(currentHealthPotions);
        currentHealth -= damage;
    }
    
    private void ResetHealth()
    {
        currentHealth = Mathf.Min(startingHealth, maxHealth);
        uiManager.IncrementHeartSprite(currentHealth);
    }
    
    public void ShootArrow()
    {
        currentArrows = Mathf.Max(0, currentArrows - 1);
        UIManager.Instance.UpdateArrowUI(currentArrows);
    }
    
    private void ShowMouse(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
    
    private void OnEnable()
    {
        playerController = GetComponent<PlayerController>();
        usePotionInput.Enable();
        usePotionInput.performed += UseHealthPotion;
        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        usePotionInput.performed -= UseHealthPotion;
        resumeButton.onClick.RemoveListener(OnResume);
        quitButton.onClick.RemoveListener(OnQuit);
    }
    
    public void Pause()
    {
        if (_gameOver) return;
        ShowMouse(true);
        Time.timeScale = 0; 
        UIManager.Instance.ShowPauseMenu(true);
    }
    
    private void OnResume()
    {
        if (_gameOver) return;
        ShowMouse(false);
        Time.timeScale = 1;
        UIManager.Instance.ShowPauseMenu(false);
    }

    private void OnQuit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    
    private void WinFunction()
    {
        if (_gameOver) return;
        if (currentButterflies >= maxButterflies && currentTime > 0 && currentHealth >= minHealth)
        {
            UIManager.Instance.ShowWinScreen();
            ShowMouse(true);
            Time.timeScale = 0;
            _gameOver = true;
        }
    }

    private void LoseFunction()
    {
        if (_gameOver) return;
        _gameOver = true;
        UIManager.Instance.ShowLoseScreen();
        ShowMouse(true);
        Time.timeScale = 0; 
    }
    
    
}
