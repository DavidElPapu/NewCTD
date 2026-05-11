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
    private GameObject itemHolded;

    private void Awake()
    {
        itemHolded = null;
    }

    public bool CanPlaceItem(GameObject item)
    {
        if(item.TryGetComponent(out ICookingObject cookingObject))
        {
            if (itemHolded != null)
            {
                if (itemHolded.TryGetComponent(out ICookingObject myCookingObject) && myCookingObject.GetCookingObjectType() == CookingObjectType.Container)
                {
                    if (itemHolded.TryGetComponent(out ContainerScript myContainer))
                    {
                        if (cookingObject.GetCookingObjectType() == CookingObjectType.Container && item.TryGetComponent(out ContainerScript container))
                        {
                            if (container.CanEmptyContainer() && container.GetContainedIngredients() != null)
                            {
                                List<IngredientScript> transferIngredients = container.GetContainedIngredients();
                                if (myContainer.CanPlaceIngredientList(transferIngredients))
                                {
                                    container.EmptyContainer();
                                    myContainer.PlaceIngredientList(transferIngredients);
                                    OnItemHold?.Invoke(itemHolded);
                                    //There is no need for return true since the player will keep the container
                                }
                            }
                        }
                        else if (cookingObject.GetCookingObjectType() == CookingObjectType.Ingredient && item.TryGetComponent(out IngredientScript aloneIngredient))
                        {
                            if (myContainer.CanPlaceIngredient(aloneIngredient))
                            {
                                myContainer.PlaceIngredient(aloneIngredient);
                                OnItemHold?.Invoke(itemHolded);
                                return true;
                            }
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
                itemHolded = item;
                OnItemHold?.Invoke(itemHolded);
                return true;
            }
        }
        return false;
    }

    public GameObject OnPickEmpty()
    {
        if (itemHolded == null) return null;
        GameObject tempHold = itemHolded;
        itemHolded = null;
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
