using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Transform selectionPoint;

    private List<GameObject> inventory;
    private int inventorySlots, selectedItem;

    private void Awake()
    {
        inventory = new List<GameObject>();
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
            GameObject station = MapGridManager.singleton.gridData.GetGameObjectAt(MapGridManager.singleton.mapGrid.WorldToCell(selectionPoint.position));
            if (station == null || !station.TryGetComponent(out IInteractable stationInteraction)) return;
            if (inventory[selectedItem] == null)
            {
                if (stationInteraction.OnPickEmpty() != null)
                {
                    inventory[selectedItem] = stationInteraction.OnPickEmpty();
                    //Missing placing item on players hand
                }
            }
            else
            {
                if (stationInteraction.CanPlaceItem(inventory[selectedItem]) == true)
                {
                    inventory[selectedItem] = null;
                    //Missing removing item from players hand

                }
            }
        }
    }

    public void OnSecondaryActionInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            GameObject station = MapGridManager.singleton.gridData.GetGameObjectAt(MapGridManager.singleton.mapGrid.WorldToCell(selectionPoint.position));
            if (station == null || !station.TryGetComponent(out IInteractable stationInteraction) || inventory[selectedItem] != null) return;
            stationInteraction.OnUse();
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
