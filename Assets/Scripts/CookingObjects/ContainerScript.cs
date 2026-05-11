using UnityEngine;
using System.Collections.Generic;

public class ContainerScript : MonoBehaviour, ICookingObject
{
    public ContainerName cName;
    [SerializeField] private List<IngredientType> validIngredientTypes;
    [SerializeField] private List<IngredientType> validIngredientStates;
    [SerializeField] private int maxContainedIngredients;
    protected List<IngredientScript> containedIngredients;

    protected virtual void Awake()
    {
        containedIngredients = new List<IngredientScript>();
    }

    #region PlaceOnContainer

    public bool CanPlaceIngredient(IngredientScript ingredient)
    {
        if (containedIngredients.Count >= maxContainedIngredients) return false;
        //For now, it just need to be either type or state, that might change later for an exeptions list (like water, ice , etc)
        if (IsIngredientTypeValid(ingredient.data.type) || IsIngredientStateValid(ingredient.data.state)) return true;
        return false;
    }

    public bool CanPlaceIngredientList(List<IngredientScript> ingredientList)
    {
        foreach (IngredientScript ingredient in ingredientList)
        {
            if (!CanPlaceIngredient(ingredient)) return false;
        }
        return true;
    }

    public virtual void PlaceIngredient(IngredientScript ingredient)
    {
        containedIngredients.Add(ingredient);
        //Missing to place ingredients, or destroy them, or hide them
    }

    public void PlaceIngredientList(List<IngredientScript> ingredientList)
    {
        foreach (IngredientScript ingredient in ingredientList)
        {
            PlaceIngredient(ingredient);
        }
    }

    #endregion

    #region EmptyContainer

    public virtual bool CanEmptyContainer()
    {
        return true;
    }

    public virtual void EmptyContainer()
    {
        containedIngredients.Clear();
        //Missing to unparent the ingredients if needed
    }

    public List<IngredientScript> GetContainedIngredients()
    {
        if (containedIngredients.Count == 0) return null;
        return containedIngredients;
    }

    #endregion

    public CookingObjectType GetCookingObjectType()
    {
        return CookingObjectType.Container;
    }

    private bool IsIngredientTypeValid(IngredientType iType)
    {
        if (validIngredientTypes.Count == 0) return true;
        foreach (IngredientType validType in validIngredientTypes)
        {
            if (iType == validType) return true;
        }
        return false;
    }

    private bool IsIngredientStateValid(IngredientState iState)
    {
        if (validIngredientStates.Count == 0) return true;
        foreach (IngredientState validState in validIngredientStates)
        {
            if (iState == validState) return true;
        }
        return false;
    }
}

public enum ContainerName
{
    Glass,
    Pot
}
