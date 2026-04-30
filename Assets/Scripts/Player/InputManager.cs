using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager singleton;
    public PlayerInputActions inputActions;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            inputActions = new PlayerInputActions();
            EnableInput();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnableInput()
    {
        inputActions.Player.Enable();
    }

    public void DisableInput()
    {
        inputActions.Player.Disable();
    }
}
