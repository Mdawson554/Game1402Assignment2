using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private GameManager gameManager;
    
    [SerializeField] private TextMeshProUGUI arrowText;
    [SerializeField] private TextMeshProUGUI healthPotionText;
    [SerializeField] private TextMeshProUGUI butterflyText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    
    //Hearts
    public List<GameObject> heartsUI = new List<GameObject>(); 
    public GameObject heartPrefab; 
    public GameObject heartSpawner;
    
    
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
        gameManager = GameManager.Instance;  
    }

    public void UpdateButterflyUI(int currentButterflies)
    {
        butterflyText.text = currentButterflies.ToString();
    }

    public void UpdateTimerUI(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateArrowUI(int currentArrows)
    {
        arrowText.text = currentArrows.ToString();
    }
    
    public void UpdateHealthPotionUI(int currentHealthPotions)
    {
        healthPotionText.text = currentHealthPotions.ToString();
    }
    
    public void ShowPauseMenu(bool show)   
    {
        pauseMenu.SetActive(show);
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }
    

    public void IncrementHeartSprite(int amount)
    {
        if (heartPrefab == null) { Debug.LogError("heartPrefab is not assigned!"); return; }
        if (heartSpawner == null) { Debug.LogError("heartSpawner is not assigned!"); return; }

        for (int i = 0; i < amount; i++) 
        {
            // parent it immediately, THEN zero the local position
            var heart = Instantiate(heartPrefab, heartSpawner.transform);
            heart.transform.localPosition = Vector3.zero;
            heartsUI.Add(heart); 
        }
    }
    
    
    public void DecrementHeartSprite(int amount)
    {
        amount = Mathf.Min(amount, heartsUI.Count); 

        for (int i = 0; i < amount; i++)
        {
            int lastIndex = heartsUI.Count - 1;   
            Destroy(heartsUI[lastIndex]);
            heartsUI.RemoveAt(lastIndex);
            Debug.Log("Heart decremented");
        }
    }
}
