using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
	private Rigidbody2D _rb;
	[SerializeField] private float _speed = 2f;

	public LayerMask groundLayer;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		_rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _speed);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (((1 << collision.gameObject.layer) & groundLayer) != 0)
		{
			_speed *= -1f;
		}
	}
}
