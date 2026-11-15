using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject Restart;
    public GameObject Exit;
    public GameObject PausePanel;
    private bool isPaused = false;

	public void RestartGame()
	{
		Time.timeScale = 1f;

		Player.spawnAtLevelStart = true;

		AudioManager.Instance.RestartBackgroundMusic();
		SceneManager.LoadScene("Level_01");
	}

	public void PauseGame() 
    {
		AudioManager.Instance.StopBackgroundMusic();
		PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

	public void ExitGame()
	{
		Application.Quit();
		Debug.Log("Saliendo del juego...");
	}
}
