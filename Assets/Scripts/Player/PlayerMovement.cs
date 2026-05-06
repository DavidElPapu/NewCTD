using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed, runSpeed, rotationSpeed;

    private Rigidbody rb;
    private Vector2 moveInput;
    private float speed;

    private void Awake()
    {
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
    }

    private void MovePlayer()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 movementVector = new Vector3(moveInput.x, 0, moveInput.y);
            rb.AddForce(movementVector * speed, ForceMode.Force);

            //Rotates the player towards movement direction
            Quaternion toRotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            //Below are alternatives to rotation that also worked, just in case
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            //rb.MoveRotation(toRotation);
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.action.ReadValue<Vector2>();
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            speed = runSpeed;
        else
            speed = walkSpeed;
    }
}
