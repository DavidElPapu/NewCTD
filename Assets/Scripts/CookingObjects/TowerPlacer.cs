using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    private ContainerScript containerScript;
    private HashSet<RecipeIngredient> containedRecipeIngredients;
    private GameObject towerPrefab;

    private void Awake()
    {
        TryGetComponent(out containerScript);
        containedRecipeIngredients = new HashSet<RecipeIngredient>();
        towerPrefab = null;
    }

    private void OnEnable()
    {
        containerScript.OnIngredientPlaced += AddIngredientToHash;
        containerScript.OnIngredientReset += ClearHash;
    }

    private void OnDisable()
    {
        containerScript.OnIngredientPlaced -= AddIngredientToHash;
        containerScript.OnIngredientReset -= ClearHash;
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
