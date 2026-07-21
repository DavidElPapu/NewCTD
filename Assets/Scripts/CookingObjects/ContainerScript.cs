using System;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : CookingObject
{
    [SerializeField] private List<IngredientState> validIngredientStates;
    [SerializeField] protected WorldspaceUIControler uiControler;
    [SerializeField] protected GameObject contentModel;
    [SerializeField] private int maxContainedIngredients;
    protected List<IngredientScript> containedIngredients;
    // This is used to change the color of the material for this object only without changing the asset
    private MaterialPropertyBlock propBlock;
    private MeshRenderer contentMeshRenderer;
    private Color contentColor;
    private ContainerUI containerUI;

    protected virtual void Awake()
    {
        uiControler.TryGetComponent(out containerUI);
        containedIngredients = new List<IngredientScript>();
        contentModel.SetActive(false);
        propBlock = new MaterialPropertyBlock();
        contentModel.TryGetComponent(out contentMeshRenderer);
        contentColor = Color.black;
        uiControler.RotateUIToCamera();
        containerUI.UpdateIngredients(containedIngredients);
    }

    #region PlaceOnContainer

    public override bool TryEnterItem(CookingObject item)
    {
        //This method only returns true if the item enters this container
        if (item is IngredientScript ingredient)
        {
            if (CanPlaceIngredient(ingredient))
            {
                PlaceIngredient(ingredient);
                return true;
            }
        }
        else if (item is ContainerScript container)
        {
            //If the container trying to enter has ingredients that can enter this container, then it places them inside and empties the other container
            if (container.CanEmptyContainer() && container.GetContainedIngredients() != null)
            {
                if (CanPlaceIngredientList(container.GetContainedIngredients()))
                {
                    PlaceIngredientList(container.GetContainedIngredients());
                    container.EmptyContainer();
                    return false;
                }
            }
            //If the other container's content couldn't enter this container, then it checks if this container's content can enter the other container
            if (CanEmptyContainer() && containedIngredients.Count > 0)
            {
                if (container.CanPlaceIngredientList(containedIngredients))
                {
                    container.PlaceIngredientList(containedIngredients);
                    EmptyContainer();
                    return false;
                }
            }
        }
        return false;
    }

    public bool CanPlaceIngredient(IngredientScript ingredient)
    {
        if (containedIngredients.Count >= maxContainedIngredients) return false;
        //For now, it needs to be a valid type and state, that might change later for an exeptions list (like water, ice , etc)
        if (IsIngredientStateValid(ingredient.state)) return true;
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
        containerUI.UpdateIngredients(containedIngredients);
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
        contentModel.SetActive(false);
        contentColor = Color.black;
        containerUI.UpdateIngredients(containedIngredients);
    }

    public List<IngredientScript> GetContainedIngredients()
    {
        if (containedIngredients.Count == 0) return null;
        return containedIngredients;
    }

    #endregion

    public override void OnPlayerInteraction(bool wasPicked)
    {
        uiControler.ToggleUIRotationUpdate(wasPicked);
        //Rotates the UI again one last time so the UI stays rotated after being placed
        if (!wasPicked)
            uiControler.RotateUIToCamera();
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
