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
    }

    private void OnEnable()
    {
        inventorySlots = 3;
        selectedItem = 0;
        InitializeInventory();
        UIManager.singleton.playerUI.InitializeInventoryUI(inventorySlots);
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
                    inventory[selectedItem].OnPlayerInteraction(true);
                    UIManager.singleton.playerUI.SetInventoryItemIcon(selectedItem, inventory[selectedItem].icon);
                }
            }
            else
            {
                if (station.TryPlaceItem(inventory[selectedItem]) == true)
                {
                    inventory[selectedItem].OnPlayerInteraction(false);
                    inventory[selectedItem] = null;
                    UIManager.singleton.playerUI.SetInventoryItemIcon(selectedItem, null);
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
        if (inventory[selectedItem] != null)
            inventory[selectedItem].gameObject.SetActive(false);
        if (context.ReadValue<float>() > 0)
        {
            if (selectedItem == 0)
                selectedItem = inventory.Count - 1;
            else
                selectedItem--;
        }
        else
        {
            if (selectedItem + 1 >= inventory.Count)
                selectedItem = 0;
            else
                selectedItem++;
        }
        if (inventory[selectedItem] != null)
            inventory[selectedItem].gameObject.SetActive(true);
        UIManager.singleton.playerUI.SwitchSelectedItem(selectedItem);
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < inventorySlots; i++)
        {
            inventory.Add(null);
        }
    }
}
