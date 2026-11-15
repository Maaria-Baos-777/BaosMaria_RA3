using System;
using System.Collections;
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
    private Animator _animator;
    private bool _isWinning = false;

    public LayerMask _groundLayer;
    public static Player _instance;
	public static bool spawnAtLevelStart = false;

	[SerializeField] private float _speed = 5f;
    [SerializeField] private float rayLength = 1f;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip _jumpSound;
	[SerializeField] private AudioClip _diamondSound;

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
        _animator = GetComponent<Animator>();
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

		_animator.SetBool("isWalking", _positionCat != Vector2.zero);
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
            _audioSource.PlayOneShot(_jumpSound);
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
        if(collision.gameObject.CompareTag("Spikes")) 
        {
            DeadCat();
        }

        if(collision.gameObject.CompareTag("Enemy")) 
        {
            DeadCat();
        }

		if (collision.gameObject.CompareTag("Bullet"))
		{
			DeadCat();
		}
	}

    public void DeadCat()
    {
		AudioManager.Instance.StopBackgroundMusic();
		AudioManager.Instance.PlayGameOverSound();
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
            _audioSource.PlayOneShot(_diamondSound);
            Destroy(collision.gameObject);
            _diamonds++;
        }

        if(collision.transform.CompareTag("NPCGirl") && !_isWinning) 
        {
            _isWinning = true;
            StartCoroutine(LoadWinGame());
        }
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	    if(scene.name == "GameOver" || scene.name == "WinGame")
        {
            Destroy(gameObject);
            return;
        }	

        if (spawnAtLevelStart)
		{
			transform.position = new Vector3(-7f, -1.418718f, transform.position.z);
			_rb.linearVelocity = Vector2.zero;
			spawnAtLevelStart = false;
		}
	}

	private IEnumerator LoadWinGame()
	{
		yield return new WaitForSeconds(3f);
		AudioManager.Instance.StopBackgroundMusic();
		AudioManager.Instance.PlayWinSound();
		SceneManager.LoadScene("WinGame");
	}
}
