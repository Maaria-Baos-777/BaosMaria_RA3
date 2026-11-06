using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, InputSystem_Actions.ICatPlayerActions
{
    private InputSystem_Actions _inputAction;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Vector2 _positionCat;
    [SerializeField] private float _speed = 5f;

    void Awake() 
    {
        _inputAction = new InputSystem_Actions();
        _inputAction.CatPlayer.SetCallbacks(this);
    }

    void Start() 
    {
		_rb = GetComponent<Rigidbody2D>();
		_sr = GetComponent<SpriteRenderer>();
	}

    void OnEnable() 
    { 
        _inputAction.Enable();
    }

    void OnDisable() 
    {
        _inputAction.Disable();
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
        Gravity();
		_sr.flipY = !_sr.flipY;
	}

    public void Gravity() 
    {
        _rb.gravityScale *= -1;

    }
}
