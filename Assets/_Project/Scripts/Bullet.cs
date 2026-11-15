using UnityEngine;

public class Bullet : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// Handle player hit logic here
			Debug.Log("Player hit!");
		}
		// Destroy the bullet on collision
		Destroy(gameObject);
	}
}
