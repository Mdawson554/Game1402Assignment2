using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int maxTime;
    [SerializeField] private int maxButterflies;
    [SerializeField] private int minHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int startingHealth;
    [SerializeField] private int healthBoostAmount;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    public float currentTime;
    public int currentHealth;
    public bool _isAtMaxHealth;
    private bool _gameOver;
    private AudioManager audioManager;
    private UIManager uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        audioManager = AudioManager.Instance;
        uiManager = UIManager.Instance;
        ResetHealth();
        currentTime = maxTime;
        ShowMouse(false);
        audioManager.PlayBGMusic();

        if (currentHealth >= maxHealth)
            _isAtMaxHealth = true;
        else
            _isAtMaxHealth = false;
    }

    private void Update()
    {
        DecreaseTimerTime();
    }

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnResume);
        quitButton.onClick.RemoveListener(OnQuit);
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

    public void CheckWinCondition()
    {
        if (InventoryManager.Instance.currentButterflies >= maxButterflies && currentTime > 0 &&
            currentHealth >= minHealth)
        {
            UIManager.Instance.ShowWinScreen();
            ShowMouse(true);
            Time.timeScale = 0;
            _gameOver = true;
        }
    }

    public void GainHealth()
    {
        var actualHeal = Mathf.Min(healthBoostAmount, maxHealth - currentHealth);
        currentHealth += actualHeal;
        uiManager.IncrementHeartSprite(actualHeal);
        if (currentHealth >= maxHealth) _isAtMaxHealth = true;
    }

    public void LooseHealth(int damage)
    {
        _isAtMaxHealth = false;
        currentHealth = Mathf.Max(currentHealth - damage, minHealth);
        uiManager.DecrementHeartSprite(damage);
        if (currentHealth <= minHealth)
            LoseFunction();
    }

    private void ResetHealth()
    {
        currentHealth = Mathf.Min(startingHealth, maxHealth);
        uiManager.IncrementHeartSprite(currentHealth);
    }

    public void ShootArrow()
    {
        InventoryManager.Instance.ShootArrow();
        UIManager.Instance.UpdateArrowUI(InventoryManager.Instance.currentArrows);
    }

    private void ShowMouse(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
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

    private void LoseFunction()
    {
        if (_gameOver) return;
        _gameOver = true;
        UIManager.Instance.ShowLoseScreen();
        ShowMouse(true);
        Time.timeScale = 0;
    }
}