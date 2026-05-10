using UnityEngine;
using System.Collections.Generic;

public class ContainerScript : MonoBehaviour, ICookingObject
{
    public ContainerName cName;
    [SerializeField] private List<IngredientType> validIngredientTypes;
    [SerializeField] private List<IngredientType> validIngredientStates;
    [SerializeField] private int maxContainedIngredients;
    private List<IngredientScript> containedIngredients;

    private void Awake()
    {
        containedIngredients = new List<IngredientScript>();
    }

    public bool CanPlaceIngredient(IngredientScript ingredient)
    {
        //Aqui hay que checar que no tenga un proceso activo (falta agregar eso tanto aqui como en cada ingrediente) y ademas otra funcion igual a esta, pero para
        //varios ingredientes, ademas que esta solo checa, no hace, entonces falta tambien la funcion de poner el ingrediente como tal
        return false;
    }

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
