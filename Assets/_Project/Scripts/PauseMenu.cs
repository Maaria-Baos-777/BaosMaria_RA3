using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject Restart;
    public GameObject PausePanel;
    private bool isPaused = false;

    public void RestartGame() 
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Level_01");
    }

    public void PauseGame() 
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
