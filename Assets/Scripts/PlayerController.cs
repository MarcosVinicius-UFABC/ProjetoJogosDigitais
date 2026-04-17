using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions input; // nome do seu asset
    private Vector2 moveInput;

    public float speed = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        input = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMove;
        input.Player.Movement.canceled += OnMove;
    }

    private void OnDisable()
    {
        input.Player.Movement.performed -= OnMove;
        input.Player.Movement.canceled -= OnMove;
        input.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
}