using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player")) 
		{
			Player.spawnAtLevelStart = true;

			int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
		}
	}
}
