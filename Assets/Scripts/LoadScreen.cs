using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    public string SceneName;
    [SerializeField] private Button PlayGameButton;
    [SerializeField] private Button QuitButton;

    void OnEnable()
    {
        if(PlayGameButton == null ||  QuitButton == null) return;
        PlayGameButton.onClick.AddListener(OnButtonClicked);
        QuitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    void OnDisable()
    {
        if(PlayGameButton == null ||  QuitButton == null) return;
        PlayGameButton.onClick.RemoveListener(OnButtonClicked);
        QuitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    public void OnButtonClicked()
    {
        SceneManager.LoadScene(SceneName);
    }

    private void OnQuitButtonClicked()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
