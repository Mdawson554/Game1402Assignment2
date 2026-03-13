using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string SceneName;
    public static GameManager Instance;
    
    [SerializeField] public int MaxButterflies;
    [SerializeField] public int MaxArrows;
    [SerializeField] private TMP_Text arrowText;
    [SerializeField] private Button playButton;
    
    public int CurrentArrows;
    public int CurrentButterflies;
    public GameObject PauseMenu;
    public GameObject GameUI;
    
    private bool ispaused;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    
    public void CollectButterflies(int butterflyGain)
    {
        CurrentButterflies = Mathf.Clamp(CurrentButterflies + butterflyGain, 0, MaxButterflies);
    }

    public void WinFunction()
    {
        if (CurrentButterflies != MaxButterflies) return;
        SceneManager.LoadScene(SceneName);
    }
    
    public void AddArrowsToInventory() 
    {
        CurrentArrows = MaxArrows;
        arrowText.text = CurrentArrows.ToString();
    }

    public void ShootArrow()
    {
        CurrentArrows = CurrentArrows - 1;
        arrowText.text = CurrentArrows.ToString();
    }
    
    public void Pause(InputAction.CallbackContext context)
    {
        if (ispaused) 
        {
            ispaused = false;
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            //enable the mouse behaviour
        }
        else
        {
            ispaused = true;
            Time.timeScale = 0; 
            PauseMenu.SetActive(true); 
            //disable the mouse behaviour
        }
    }
}
