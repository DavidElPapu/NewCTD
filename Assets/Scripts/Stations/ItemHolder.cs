using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemHolder : MonoBehaviour, IPlaceable
{
    public event Action<GameObject> OnItemHold;
    public event Action<GameObject> OnItemLeave;
    [SerializeField] private List<CookingObjectName> validItems;
    [SerializeField] private List<IngredientType> validIngredientTypes;
    [SerializeField] private List<IngredientState> validIngredientStates;
    [SerializeField] private Transform itemLocation;
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
                if (item.TryGetComponent(out ContainerScript container))
                {
                    if (itemHeld.TryGetComponent(out ContainerScript myContainer))
                    {
                        if (container.GetContainedIngredients() == null && myContainer.GetContainedIngredients() != null && myContainer.CanEmptyContainer())
                        {
                            List<IngredientScript> transferIngredients = new List<IngredientScript>(myContainer.GetContainedIngredients());
                            if (container.CanPlaceIngredientList(transferIngredients))
                            {
                                myContainer.EmptyContainer();
                                container.PlaceIngredientList(transferIngredients);
                                OnItemLeave?.Invoke(itemHeld);
                            }
                        }
                        else if(container.GetContainedIngredients() != null && container.CanEmptyContainer())
                        {
                            List<IngredientScript> transferIngredients = new List<IngredientScript>(container.GetContainedIngredients());
                            if (myContainer.CanPlaceIngredientList(transferIngredients))
                            {
                                container.EmptyContainer();
                                myContainer.PlaceIngredientList(transferIngredients);
                                OnItemHold?.Invoke(itemHeld);
                            }
                            //This code below could be a more efficient way of switching List, but container.Empty container might need tweaks to not show objects
                            //if (myContainer.CanPlaceIngredientList(container.GetContainedIngredients()))
                            //{
                            //    myContainer.PlaceIngredientList(container.GetContainedIngredients());
                            //    container.EmptyContainer();
                            //    OnItemHold?.Invoke(itemHeld);
                            //}
                        }
                    }
                    else if (itemHeld.TryGetComponent(out IngredientScript myAloneIngredient) && container.CanPlaceIngredient(myAloneIngredient))
                    {
                        container.PlaceIngredient(myAloneIngredient);
                        OnItemLeave?.Invoke(itemHeld);
                        itemHeld = null;
                    }
                }
                else
                {
                    if (itemHeld.TryGetComponent(out ContainerScript myContainer) && item.TryGetComponent(out IngredientScript aloneIngredient))
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
                if (!IsCookingObjectValid(cookingObject.GetCookingObjectName()))
                    if (!IsIngredientValid(item)) return false;
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

    private bool IsCookingObjectValid(CookingObjectName item)
    {
        if (validItems.Count != 0)
        {
            foreach (CookingObjectName validItem in validItems)
            {
                if (item == validItem) return true;
            }
        }
        return false;
    }

    private bool IsIngredientValid(GameObject possibleIngredient)
    {
        //For now, if there is a valid type, there needs to be valid state and viceversa
        if (validIngredientTypes.Count == 0 || validIngredientStates.Count == 0) return true;

        if (possibleIngredient.TryGetComponent(out IngredientScript ingredient))
        {
            foreach (IngredientType validType in validIngredientTypes)
            {
                if (ingredient.data.type == validType) return true;
            }
        }
        return false;
    }
}
