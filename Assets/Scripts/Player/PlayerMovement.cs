using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions inputActions;
    [SerializeField] private float walkSpeed, runSpeed, rotationSpeed;
    private Rigidbody rb;

    private float speed;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    private void OnEnable()
    {
        //Commented 2 lines below this because the input creation was moved to a singleton in InputManager script
        //inputActions = new PlayerInputActions();
        //inputActions.Player.Enable();
        inputActions = InputManager.singleton.inputActions;
        inputActions.Player.Run.performed += OnRunInput;
        inputActions.Player.Run.canceled += OnRunInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Run.performed -= OnRunInput;
        inputActions.Player.Run.canceled -= OnRunInput;
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
        Vector2 moveInput = InputManager.singleton.inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(moveInput.x, 0, moveInput.y);

        rb.AddForce(movementVector * speed, ForceMode.Force);

        //Rotates the player towards movement direction
        if (movementVector != Vector3.zero) 
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            //Below are alternatives to rotation that also worked, just in case
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            //rb.MoveRotation(toRotation);
        }
    }

    private void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            speed = runSpeed;
        else
            speed = walkSpeed;
    }
}
