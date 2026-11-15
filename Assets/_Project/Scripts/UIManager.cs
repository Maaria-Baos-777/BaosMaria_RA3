using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public void StartGame()
	{
		Time.timeScale = 1f;
		Player.spawnAtLevelStart = true;
		AudioManager.Instance.RestartBackgroundMusic();
		SceneManager.LoadScene("Level_01");
	}

	public void RestartGame()
	{
		Time.timeScale = 1f;

		Player.spawnAtLevelStart = true;

		AudioManager.Instance.RestartBackgroundMusic();
		SceneManager.LoadScene("Level_01");
	}

	public void GoToMenu()
	{
		AudioManager.Instance.StopBackgroundMusic();
		SceneManager.LoadScene("Menu");
	}
	public void ExitGame()
	{
		Application.Quit();

		Debug.Log("Exiting the game...");
	}
}
