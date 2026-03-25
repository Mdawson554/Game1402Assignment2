using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    public string SceneName;
    [SerializeField] private Button playGameButton;
    [SerializeField] private Button quitButton;

    void OnEnable()
    {
        if(playGameButton == null ||  quitButton == null) return;
        playGameButton.onClick.AddListener(OnButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    void OnDisable()
    {
        if(playGameButton == null ||  quitButton == null) return;
        playGameButton.onClick.RemoveListener(OnButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
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
