using TMPro;
using UnityEngine;

public class Toast : MonoBehaviour
{
    public static Toast Instance;

    [SerializeField] private GameObject toastUI;
    [SerializeField] private TMPro.TextMeshProUGUI toastText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }
    void Start()
    {
        toastUI.SetActive(false);
    }
    
    public void ShowToast(string textValue)
    {
        toastUI.SetActive(true);
        toastText.SetText(textValue);
    }
    
    public void HideToast()
    {
        toastUI.SetActive(false);
    }
}
