using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed, runSpeed, rotationSpeed, staminaRecoveryRate, staminaConsumptionRate;
    [SerializeField] private int maxStamina;

    private Rigidbody rb;
    private Vector2 moveInput;
    private float speed, staminaCount;
    private int currentStamina;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    private void OnEnable()
    {
        speed = walkSpeed;
        currentStamina = maxStamina;
        UIManager.singleton.playerUI.InitializeStaminaUI(maxStamina);
    }

    private void Update()
    {
        UpdateStamina();
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

    private void UpdateStamina()
    {
        staminaCount -= Time.fixedDeltaTime;
        if (staminaCount <= 0)
        {
            //If the player is running, remove stamina
            if (speed == walkSpeed && currentStamina < maxStamina)
            {
                currentStamina++;
                staminaCount = staminaRecoveryRate;
            }
            else if (speed == runSpeed && currentStamina > 0)
            {
                currentStamina--;
                staminaCount = staminaConsumptionRate;
                if (currentStamina <= 0)
                    speed = walkSpeed;
            }
            else
                return;
            UIManager.singleton.playerUI.ChangeStaminaSliderValue(currentStamina);
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.action.ReadValue<Vector2>();
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            speed = runSpeed;
            staminaCount = staminaConsumptionRate;
        }
        else
        {
            speed = walkSpeed;
            staminaCount = staminaRecoveryRate;
        }
    }
}
