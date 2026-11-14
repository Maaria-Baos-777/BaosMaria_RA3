using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, InputSystem_Actions.ICatPlayerActions
{
    private InputSystem_Actions _inputAction;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private InputAction _up;
    private Vector2 _positionCat;
    private bool _isGravityFlipped;
    private int _diamonds;

    public LayerMask _groundLayer;
    public Player _instance;
	public static bool spawnAtLevelStart = false;

	[SerializeField] private float _speed = 5f;
    [SerializeField] private float rayLength = 1f;

    void Awake()
    {
        _inputAction = new InputSystem_Actions();
        _inputAction.CatPlayer.SetCallbacks(this);

        if(_instance == null) 
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _up = InputSystem.actions.FindAction("up");
    }

    void OnEnable()
    {
        _inputAction.Enable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
    {
        _inputAction.Disable();
		SceneManager.sceneLoaded -= OnSceneLoaded;

	}

	void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_positionCat.x * _speed, _rb.linearVelocityY);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _positionCat = context.ReadValue<Vector2>();
        if (_positionCat.x > 0)
        {
            _sr.flipX = false;
        }
        else if (_positionCat.x < 0)
        {
            _sr.flipX = true;
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (IsGrounded())
        {
            Gravity();
            _sr.flipY = !_sr.flipY;
        }
    }

    public void Gravity()
    {
        _isGravityFlipped = !_isGravityFlipped;
        _rb.gravityScale *= -1;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pinchos"))
        {
            DeadCat();
        }
        
        if(collision.gameObject.CompareTag("Spikes")) 
        {
            DeadCat();
        }

        if(collision.gameObject.CompareTag("Enemy")) 
        {
            DeadCat();
        }
    }

    public void DeadCat()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    private bool IsGrounded()
    {
        Vector2 direction = _rb.gravityScale > 0 ? Vector2.down : Vector2.up;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, _groundLayer);
        return hit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Diamond"))
        {
            Destroy(collision.gameObject);
            _diamonds++;
        }
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (spawnAtLevelStart)
		{
			transform.position = new Vector3(-7f, -1.418718f, transform.position.z);

			_rb.linearVelocity = Vector2.zero;

			spawnAtLevelStart = false;
		}
	}
}
