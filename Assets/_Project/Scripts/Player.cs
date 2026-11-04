using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, InputSystem_Actions.ICatPlayerActions
{
    private InputSystem_Actions _inputAction;
    private Rigidbody2D _rb;

    void Awake() 
    {
        _inputAction = new InputSystem_Actions();
        _inputAction.CatPlayer.SetCallbacks(this);
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 maria;

        maria = context.ReadValue<Vector2>();

    }

    public void OnUp(InputAction.CallbackContext context) 
    { 
    
    }
}
