using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemHolder : MonoBehaviour, IPlaceable
{
    public event Action<GameObject> OnItemHold;
    public event Action<GameObject> OnItemLeave;
    [SerializeField] private Transform itemLocation;
    //For now stations can only hold specific containers, not specific ingredients or specific something else
    [SerializeField] private List<ContainerName> validContainers;
    private GameObject itemHeld;

    private void Awake()
    {
        itemHeld = null;
    }

    public bool CanPlaceItem(GameObject item)
    {
        if(item.TryGetComponent(out ICookingObject cookingObject))
        {
            if (itemHeld != null)
            {
                if (itemHeld.TryGetComponent(out ContainerScript myContainer))
                {
                    if (item.TryGetComponent(out ContainerScript container))
                    {
                        if (container.CanEmptyContainer() && container.GetContainedIngredients() != null)
                        {
                            List<IngredientScript> transferIngredients = container.GetContainedIngredients();
                            if (myContainer.CanPlaceIngredientList(transferIngredients))
                            {
                                container.EmptyContainer();
                                myContainer.PlaceIngredientList(transferIngredients);
                                OnItemHold?.Invoke(itemHeld);
                                //There is no need for return true since the player will keep the container
                            }
                        }
                    }
                    else if (item.TryGetComponent(out IngredientScript aloneIngredient))
                    {
                        if (myContainer.CanPlaceIngredient(aloneIngredient))
                        {
                            myContainer.PlaceIngredient(aloneIngredient);
                            OnItemHold?.Invoke(itemHeld);
                            return true;
                        }
                    }
                }
                //For now, you can't mix ingredients outside a container
            }
            else
            {
                if (cookingObject.GetCookingObjectType() == CookingObjectType.Container && item.TryGetComponent(out ContainerScript container))
                    if (!IsContainerValid(container.cName)) return false;
                item.transform.parent = null;
                item.transform.position = itemLocation.position;
                item.transform.rotation = itemLocation.rotation;
                itemHeld = item;
                OnItemHold?.Invoke(itemHeld);
                return true;
            }
        }
        return false;
    }

    public GameObject OnPickEmpty()
    {
        if (itemHeld == null) return null;
        GameObject tempHold = itemHeld;
        itemHeld = null;
        OnItemLeave?.Invoke(tempHold);
        return tempHold;
    }

    private bool IsContainerValid(ContainerName container)
    {
        if (validContainers.Count == 0) return true;
        foreach (ContainerName validContainer in validContainers)
        {
            if (container == validContainer) return true;
        }
        return false;
    }
}
