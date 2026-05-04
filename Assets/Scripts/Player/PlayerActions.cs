using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    private List<GameObject> inventory;
    private int inventorySlots;

    private void Awake()
    {
        inventory = new List<GameObject>();
        inventorySlots = 3;
    }

    private void OnEnable()
    {
        InputManager.singleton.inputActions.Player.Use.performed += OnMainAction;
        InputManager.singleton.inputActions.Player.Use.performed += OnSecondaryAction;
        InputManager.singleton.inputActions.Player.Use.canceled += OnSecondaryAction;
        InputManager.singleton.inputActions.Player.Scroll.performed += OnScroll;
    }

    private void OnDisable()
    {
        InputManager.singleton.inputActions.Player.Use.performed -= OnMainAction;
        InputManager.singleton.inputActions.Player.Use.performed -= OnSecondaryAction;
        InputManager.singleton.inputActions.Player.Use.canceled -= OnSecondaryAction;
        InputManager.singleton.inputActions.Player.Scroll.performed -= OnScroll;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMainAction(InputAction.CallbackContext context)
    {

    }

    private void OnSecondaryAction(InputAction.CallbackContext context)
    {

    }

    private void OnScroll(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
    }
}
