using System;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour, ICookingObject
{
    public event Action<IngredientScript> OnIngredientPlaced;
    public event Action OnIngredientReset;
    public CookingObjectName cName;
    [SerializeField] private List<IngredientType> validIngredientTypes;
    [SerializeField] private List<IngredientState> validIngredientStates;
    [SerializeField] protected GameObject contentModel;
    [SerializeField] private int maxContainedIngredients;
    protected List<IngredientScript> containedIngredients;
    // This is used to change the color of the material for this object only without changing the asset
    private MaterialPropertyBlock propBlock;
    private MeshRenderer contentMeshRenderer;
    private Color contentColor;

    protected virtual void Awake()
    {
        containedIngredients = new List<IngredientScript>();
        contentModel.SetActive(false);
        propBlock = new MaterialPropertyBlock();
        contentModel.TryGetComponent(out contentMeshRenderer);
        contentColor = Color.black;
    }

    #region PlaceOnContainer

    public bool CanPlaceIngredient(IngredientScript ingredient)
    {
        if (containedIngredients.Count >= maxContainedIngredients) return false;
        //For now, it needs to be a valid type and state, that might change later for an exeptions list (like water, ice , etc)
        if (IsIngredientTypeValid(ingredient.data.type) && IsIngredientStateValid(ingredient.state)) return true;
        return false;
    }

    public bool CanPlaceIngredientList(List<IngredientScript> ingredientList)
    {
        if (containedIngredients.Count + ingredientList.Count > maxContainedIngredients) return false;
        foreach (IngredientScript ingredient in ingredientList)
        {
            if (!CanPlaceIngredient(ingredient)) return false;
        }
        return true;
    }

    public virtual void PlaceIngredient(IngredientScript ingredient)
    {
        containedIngredients.Add(ingredient);
        OnIngredientPlaced?.Invoke(ingredient);
        //For now it just deactivates the gameObject
        ingredient.transform.parent = transform;
        ingredient.transform.position = transform.position;
        ingredient.gameObject.SetActive(false);
        if (!contentModel.activeSelf)
            contentModel.SetActive(true);
        if (contentColor == Color.black)
            contentColor = ingredient.data.baseColor;
        else
            contentColor = Color.Lerp(contentColor, ingredient.data.baseColor, 0.5f);

        //Changes the color of the material of this specific object by changing the property Block for this object
        contentMeshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_BaseColor", contentColor);
        contentMeshRenderer.SetPropertyBlock(propBlock);
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
        OnIngredientReset?.Invoke();
        contentModel.SetActive(false);
        contentColor = Color.black;
    }

    public List<IngredientScript> GetContainedIngredients()
    {
        if (containedIngredients.Count == 0) return null;
        return containedIngredients;
    }

    #endregion

    public CookingObjectName GetCookingObjectName()
    {
        return cName;
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
