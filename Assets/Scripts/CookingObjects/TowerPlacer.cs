using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : ContainerScript
{
    private HashSet<RecipeIngredient> containedRecipeIngredients;
    public GameObject towerPrefab;

    protected override void Awake()
    {
        base.Awake();
        containedRecipeIngredients = new HashSet<RecipeIngredient>();
        towerPrefab = null;
    }

    public override void PlaceIngredient(IngredientScript ingredient)
    {
        base.PlaceIngredient(ingredient);
        AddIngredientToHash(ingredient);
    }

    public override void EmptyContainer()
    {
        base.EmptyContainer();
        ClearHash();
    }

    private void AddIngredientToHash(IngredientScript ingredient)
    {
        RecipeIngredient newRecipeIngredient = new RecipeIngredient(ingredient.data.iName, ingredient.state);
        if (containedRecipeIngredients.Contains(newRecipeIngredient))
        {
            //For now, recipes can't have repeated ingredients
            towerPrefab = null;
            return;
        }
        containedRecipeIngredients.Add(newRecipeIngredient);
        CheckRecipe();
    }

    private void ClearHash()
    {
        containedRecipeIngredients.Clear();
        towerPrefab = null;
    }

    private void CheckRecipe()
    {
        towerPrefab = RecipesManager.singleton.GetContentRecipePrefab(containedRecipeIngredients);
    }
}
