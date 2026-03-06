using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool ispaused;
    
    [SerializeField] public GameObject PauseButton;
    
    public void Pause()
    {
        if (ispaused) 
        {
            ispaused = false;
            Time.timeScale = 1;
            PauseMenu.SetActive(false); //makes sure that the pause menu doesn't sit over the screen despite gameplay continuing
        }
        else
        {
            ispaused = true;
            Time.timeScale = 0; //most importnant part here is the time scale!!!!!
            PauseMenu.SetActive(true); //enables the pause menu screen to overlay
        }
    }
}
