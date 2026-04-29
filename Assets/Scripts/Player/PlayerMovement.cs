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
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        TryGetComponent(out rb);
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

        //Limitar la velocidad de movimiento
        //Vector3 limitedVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        //limitedVelocity.x = Mathf.Clamp(moveInput.x * speed, -speedLimiter * speed, speedLimiter * speed);
        //limitedVelocity.z = Mathf.Clamp(moveInput.y * speed, -speedLimiter * speed, speedLimiter * speed);
        //rb.linearVelocity = limitedVelocity;
        print(rb.linearVelocity);
        //playerVelocity = PlayerRigidBody.linearVelocity;
        //playerVelocity.x = Mathf.Clamp(playerVelocity.x, -PlayerMaxSpeed * PlayerRunSpeedFake * Mathf.Abs(movementInput.x), PlayerMaxSpeed * PlayerRunSpeedFake * Mathf.Abs(movementInput.x));
        //playerVelocity.z = Mathf.Clamp(playerVelocity.z, -PlayerMaxSpeed * PlayerRunSpeedFake * Mathf.Abs(movementInput.z), PlayerMaxSpeed * PlayerRunSpeedFake * Mathf.Abs(movementInput.z));
        //playerVelocity.y = 0;
        ////if (horizontalInput == 0f || verticalInput == 0f)
        ////{
        ////    PlayerRunSpeedFake = 1;
        ////}
        //if (horizontalInput == 0)
        //{
        //    playerVelocity.x *= 0.5f;
        //}
        //if (verticalInput == 0)
        //{
        //    playerVelocity.z *= 0.5f;
        //}
        //PlayerRigidBody.linearVelocity = playerVelocity;
    }

    private void RotatePlayer()
    {
        
    }
}
