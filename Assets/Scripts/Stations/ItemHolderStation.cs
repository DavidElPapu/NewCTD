using System.Collections.Generic;
using UnityEngine;

public class ItemHolderStation : StationScript
{
    [SerializeField] private List<CookingObjectName> validItems;
    [SerializeField] private List<IngredientState> validIngredientStates;
    [SerializeField] private Transform itemLocation;
    protected CookingObject itemHeld;

    protected virtual void Awake()
    {
        itemHeld = null;
    }

    public override bool TryPlaceItem(CookingObject item)
    {
        bool couldItemEnter = false;
        if (itemHeld != null)
        {
            //If the station is holding an item, checks if the item can enter the item held
            couldItemEnter = itemHeld.TryEnterItem(item);
            //Security check, if the item couldn't enter, but item held was gone, ensures item held is null
            if (!couldItemEnter && itemHeld.transform.parent != null)
                itemHeld = null;
            UpdateItemChange();
        }
        else
        {
            //If the station isn't holding anything, checks if "item" is a valid item or valid ingredient state (if it's an ingredient)
            if (IsCookingObjectValid(item.cName) && IsIngredientStateValid(item))
            {
                //If the item is valid, places it inside this station
                PlaceItem(item);
                UpdateItemChange();
                couldItemEnter = true;
            }
            else
            {
                //If the item can't enter and item is a container, checks if its content can enter
                if (item is ContainerScript container && container.CanEmptyContainer())
                {
                    //For now the stations can only hold 1 item so the content needs to be just 1 ingredient
                    List<IngredientScript> transferIngredients = container.GetContainedIngredients();
                    if (transferIngredients != null && transferIngredients.Count == 1 && IsIngredientStateValid(transferIngredients[0]) && IsCookingObjectValid(transferIngredients[0].cName))
                    {
                        //If the ingredient inside is valid, it enters in the station
                        container.EmptyContainer();
                        PlaceItem(transferIngredients[0]);
                        UpdateItemChange();
                    }
                }
            }
        }
        return couldItemEnter;
    }

    private void PlaceItem(CookingObject item)
    {
        item.transform.parent = null;
        item.transform.position = itemLocation.position;
        item.transform.rotation = itemLocation.rotation;
        if (!item.gameObject.activeSelf)
            item.gameObject.SetActive(true);
        itemHeld = item;
    }

    public override CookingObject TryGetItem()
    {
        if (itemHeld == null) return null;
        CookingObject tempHeld = itemHeld;
        itemHeld = null;
        UpdateItemChange();
        return tempHeld;
    }

    protected virtual void UpdateItemChange()
    {
        //Nothing
    }

    public override void UseStation(CookingObject item)
    {
        //Nothing
    }

    #region ItemValidation
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
    #endregion
}
