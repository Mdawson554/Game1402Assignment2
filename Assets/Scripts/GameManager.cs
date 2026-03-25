using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private AudioManager audioManager;
    
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
    private bool _gameOver = false;
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
            LoseFunction();
            return;
        }
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void CollectButterflies(int butterflyGain)
    {
        currentButterflies += butterflyGain;
        butterflyText.text = currentButterflies.ToString();
        WinFunction();
    }
    
    public void AddArrowsToInventory() 
    {
        currentArrows = Mathf.Min(currentArrows + arrowPickupAmount, maxArrows);
        arrowText.text = currentArrows.ToString();
    }

    public void ShootArrow()
    {
        currentArrows = Mathf.Max(0, currentArrows - 1);
        arrowText.text = currentArrows.ToString();
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
        pauseMenu.SetActive(true); 
    }
    
    private void OnResume()
    {
        if (_gameOver) return;
        ShowMouse(false);
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
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
            winScreen.SetActive(true);
            ShowMouse(true);
            Time.timeScale = 0;
            _gameOver = true;
        }
    }

    private void LoseFunction()
    {
        if (_gameOver) return;
        _gameOver = true;
        loseScreen.SetActive(true);
        ShowMouse(true);
        Time.timeScale = 0; 
    }
}
