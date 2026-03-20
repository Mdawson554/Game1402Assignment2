using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string SceneName;
    public static GameManager Instance;
    private AudioManager audioManager;
    
    [SerializeField] private int maxtime;
    [SerializeField] private int maxButterflies;
    [SerializeField] private int maxArrows;
    [SerializeField] private TextMeshProUGUI arrowText;
    [SerializeField] private TextMeshProUGUI butterflyText;
    [SerializeField] private TextMeshProUGUI timerText;
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    public float currentTime;
    public int CurrentArrows;
    public int CurrentButterflies;
    public GameObject PauseMenu;
    public GameObject WinScreeen;
    public GameObject LooseScreen;
    
    public GameObject GameUI;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    
    private void Start()
    { 
        currentTime = maxtime;
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
        if (currentTime <= 0)
        {
            LooseFunction();
        }
        currentTime -= Time.deltaTime;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void CollectButterflies(int butterflyGain)
    {
        CurrentButterflies =+ butterflyGain;
        butterflyText.text = CurrentButterflies.ToString();
        WinFunction();
    }
    
    public void AddArrowsToInventory() 
    {
        CurrentArrows = maxArrows;
        arrowText.text = CurrentArrows.ToString();
    }

    public void ShootArrow()
    {
        CurrentArrows = CurrentArrows - 1;
        arrowText.text = CurrentArrows.ToString();
    }
    
    public void ShowMouse(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
    
    void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(OnQuit);
    }

    void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnResume);
        quitButton.onClick.RemoveListener(OnQuit);
    }
    
    public void Pause()
    {
        ShowMouse(true);
        Time.timeScale = 0; 
        PauseMenu.SetActive(true); 
    }
    
    public void OnResume()
    {
        ShowMouse(false);
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void OnQuit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    
    public void WinFunction()
    {
        if (CurrentButterflies < maxButterflies && currentTime > 0)
        {
            WinScreeen.SetActive(true);
            ShowMouse(true);
            Time.timeScale = 0; 
        }
    }

    public void LooseFunction()
    {
            LooseScreen.SetActive(true);
            ShowMouse(true);
            Time.timeScale = 0; 
    }
}
