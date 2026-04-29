using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions inputActions;
    [SerializeField] private float speedLimiter, walkSpeed, runSpeed, rotationSpeed;
    private Rigidbody rb;

    private float speed;

    private void Awake()
    {
        TryGetComponent(out rb);
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Run.performed += OnRunInput;
    }

    private void Start()
    {
        speed = walkSpeed;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        rb.AddForce(movementVector * speed, ForceMode.Force);
    }

    private void RotatePlayer()
    {
        
    }

    private void OnRunInput(InputAction.CallbackContext context)
    {
        print(context.phase);
    }
}
