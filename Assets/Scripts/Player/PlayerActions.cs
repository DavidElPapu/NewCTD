using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Transform selectionPoint;

    private List<CookingObject> inventory;
    private int inventorySlots, selectedItem;

    private void Awake()
    {
        inventory = new List<CookingObject>();
        inventorySlots = 3;
        selectedItem = 0;
    }

    private void Start()
    {
        InitializeInventory();
    }

    public void OnMainActionInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            StationScript station = MapGridManager.singleton.GetStationAt(selectionPoint.position);
            if (!station) return;
            if (inventory[selectedItem] == null)
            {
                inventory[selectedItem] = station.TryGetItem();
                if (inventory[selectedItem] != null)
                {
                    inventory[selectedItem].transform.parent = selectionPoint;
                    inventory[selectedItem].transform.position = selectionPoint.position;
                    inventory[selectedItem].transform.rotation = selectionPoint.rotation;
                }
            }
            else
            {
                if (station.TryPlaceItem(inventory[selectedItem]) == true)
                {
                    inventory[selectedItem] = null;
                }
            }
        }
    }

    public void OnSecondaryActionInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            StationScript station = MapGridManager.singleton.GetStationAt(selectionPoint.position);
            if (!station) return;
            station.UseStation(inventory[selectedItem]);
        }
    }

    public void OnScrollInput(InputAction.CallbackContext context)
    {
        if (inventory.Count == 0 || context.ReadValue<float>() == 0) return;
        if (context.ReadValue<float>() > 0)
        {
            if (selectedItem + 1 >= inventory.Count)
                selectedItem = 0;
            else
                selectedItem++;
        }
        else
        {
            if (selectedItem == 0)
                selectedItem = inventory.Count - 1;
            else
                selectedItem--;
        }
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < inventorySlots; i++)
        {
            inventory.Add(null);
        }
    }
}
