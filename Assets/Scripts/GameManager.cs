using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private AudioManager audioManager;
    private UIManager uiManager;
    
    [SerializeField] private string sceneName;
    [SerializeField] private int maxTime;
    [SerializeField] private int maxButterflies;
    [SerializeField] private int maxArrows;
    [SerializeField] private int arrowPickupAmount;
    [SerializeField] private TextMeshProUGUI arrowText;
    [SerializeField] private TextMeshProUGUI butterflyText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    public float currentTime;
    public int currentArrows;
    public int currentButterflies;
    private bool _gameOver;
    public GameObject pauseMenu;
    public GameObject winScreen;
    public GameObject loseScreen;
    
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
        currentTime = maxTime;
        ShowMouse(false);
        audioManager = AudioManager.Instance;
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
        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
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
        if (currentButterflies >= maxButterflies && currentTime > 0)
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
