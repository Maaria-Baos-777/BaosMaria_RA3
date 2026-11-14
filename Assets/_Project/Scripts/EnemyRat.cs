using UnityEngine;

public class EnemyRat : MonoBehaviour
{
	private Rigidbody2D _rb;
	private SpriteRenderer _sr;

	[SerializeField] private float speed = 2f;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_sr = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (speed > 0)
		{
			_sr.flipX = false;
		}
		else if (speed < 0)
		{
			_sr.flipX = true;
		}
	}

	private void FixedUpdate()
	{
		_rb.linearVelocity = new Vector2(speed, _rb.linearVelocity.y);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("WalkingInverted"))
		{
			speed *= -1f;
		}
	}
}
