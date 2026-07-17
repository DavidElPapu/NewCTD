using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemHolder : MonoBehaviour, IPlaceable
{
    [SerializeField] private List<CookingObjectName> validItems;
    [SerializeField] private List<IngredientState> validIngredientStates;
    [SerializeField] private Transform itemLocation;
    private CookingObject itemHeld;
    private IHoldProcessor holdProcessor;

    private void Awake()
    {
        itemHeld = null;
        TryGetComponent(out holdProcessor);
    }

    //This method checks item combinations to ensure the item can be placed
    public bool CanPlaceItem(CookingObject item)
    {
        if (itemHeld != null)
        {
            //If a container is trying to be placed then...
            if (item is ContainerScript container)
            {
                //If the station is holding a container, then...
                if (itemHeld is ContainerScript myContainer)
                {
                    if (container.GetContainedIngredients() == null && myContainer.GetContainedIngredients() != null && myContainer.CanEmptyContainer())
                    {
                        List<IngredientScript> transferIngredients = new List<IngredientScript>(myContainer.GetContainedIngredients());
                        if (container.CanPlaceIngredientList(transferIngredients))
                        {
                            myContainer.EmptyContainer();
                            container.PlaceIngredientList(transferIngredients);
                            ProcessorOnItemExit(itemHeld);
                        }
                    }
                    else if (container.GetContainedIngredients() != null && container.CanEmptyContainer())
                    {
                        List<IngredientScript> transferIngredients = new List<IngredientScript>(container.GetContainedIngredients());
                        if (myContainer.CanPlaceIngredientList(transferIngredients))
                        {
                            container.EmptyContainer();
                            myContainer.PlaceIngredientList(transferIngredients);
                            ProcessorOnItemEnter(itemHeld);
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
                else if (itemHeld is IngredientScript myAloneIngredient && container.CanPlaceIngredient(myAloneIngredient))
                {
                    container.PlaceIngredient(myAloneIngredient);
                    ProcessorOnItemExit(itemHeld);
                    itemHeld = null;
                }
            }
            else
            {
                //If it's holding a container, then only if an ingredient is being placed and the ingredient can be placed into the container, then is valid
                if (itemHeld is ContainerScript myContainer && item is IngredientScript aloneIngredient)
                {
                    if (myContainer.CanPlaceIngredient(aloneIngredient))
                    {
                        myContainer.PlaceIngredient(aloneIngredient);
                        ProcessorOnItemEnter(itemHeld);
                        return true;
                    }
                }
            }
            //For now, you can't mix ingredients outside a container
        }
        else
        {
            //If the station isn't holding anything, checks if "item" is a valid item or valid ingredient state (if it's an ingredient)
            if (!IsCookingObjectValid(item.cName) || !IsIngredientStateValid(item))
            {
                //I dont like how this looks, but this is to place the ingredient from a container inside this station (if "item" is a container)
                if (item is ContainerScript container2 && container2.GetContainedIngredients() != null && container2.CanEmptyContainer())
                {
                    IngredientScript newHeldIngredient = container2.GetContainedIngredients()[0];
                    if (container2.GetContainedIngredients().Count == 1 && IsCookingObjectValid(newHeldIngredient.cName))
                    {
                        if (IsIngredientStateValid(newHeldIngredient))
                        {
                            container2.EmptyContainer();
                            newHeldIngredient.gameObject.transform.parent = null;
                            newHeldIngredient.gameObject.transform.position = itemLocation.position;
                            newHeldIngredient.gameObject.transform.rotation = itemLocation.rotation;
                            if (!newHeldIngredient.gameObject.activeSelf)
                                newHeldIngredient.gameObject.SetActive(true);
                            itemHeld = newHeldIngredient;
                            ProcessorOnItemEnter(itemHeld);
                        }
                    }
                }
                return false;
            }
            item.transform.parent = null;
            item.transform.position = itemLocation.position;
            item.transform.rotation = itemLocation.rotation;
            itemHeld = item;
            ProcessorOnItemEnter(itemHeld);
            return true;
        }
        return false;
    }

    public CookingObject OnPickEmpty()
    {
        if (itemHeld == null) return null;
        CookingObject tempHeld = itemHeld;
        itemHeld = null;
        ProcessorOnItemExit(tempHeld);
        return tempHeld;
    }

    private void ProcessorOnItemEnter(CookingObject item) => holdProcessor?.OnItemEnter(item);
    private void ProcessorOnItemExit(CookingObject item) => holdProcessor?.OnItemExit(item);

    private bool IsCookingObjectValid(CookingObjectName item)
    {
        if (validItems.Count == 0) return true;
        foreach (CookingObjectName validItem in validItems)
        {
            if (item == validItem) return true;
        }
        return false;
    }

    private bool IsIngredientStateValid(CookingObject possibleIngredient)
    {
        if (validIngredientStates.Count == 0) return true;

        if (possibleIngredient is IngredientScript ingredient)
        {
            foreach (IngredientState validState in validIngredientStates)
            {
                if (ingredient.state == validState) return true;
            }
        }
        return false;
    }
}
