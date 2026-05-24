using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Recipe", menuName = "Create Recipe SO")]
public class RecipeSO : ScriptableObject
{
    public GameObject towerPrefab;
    public List<RecipeIngredient> requiredIngredients;
}

[Serializable]
public struct RecipeIngredient
{
    public CookingObjectName name;
    public IngredientState state;

    public RecipeIngredient(CookingObjectName name, IngredientState state)
    {
        this.name = name;
        this.state = state;
    }
}
