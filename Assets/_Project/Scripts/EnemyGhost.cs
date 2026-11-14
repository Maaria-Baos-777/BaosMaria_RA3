using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField] private float speed = 2f;

	public LayerMask groundLayer;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (((1 << collision.gameObject.layer) & groundLayer) != 0)
		{
			speed *= -1f;
		}
	}
}
