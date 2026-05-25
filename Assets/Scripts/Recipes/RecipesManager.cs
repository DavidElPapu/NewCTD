using System.Collections.Generic;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    public static RecipesManager singleton;
    [SerializeField] private List<RecipeSO> allRecipesSO = new List<RecipeSO>();
    private HashSet<RecipeScript> allRecipes, levelRecipes;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            SetAllRecipes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetAllRecipes()
    {
        allRecipes = new HashSet<RecipeScript>();
        foreach (RecipeSO recipeSO in allRecipesSO)
        {
            allRecipes.Add(new RecipeScript(recipeSO, recipeSO.requiredIngredients));
        }
    }

    private void FilterRecipes(List<CookingObjectName> selectedIngredients)
    {
        //foreach (CookingObjectName ingredient in selectedIngredients)
        //{
        //    RecipeIngredient 
        //}
        //foreach (RecipeScript recipe in levelRecipes)
        //{
        //    if (recipe.ingredients.Contains())
        //}
    }

    public GameObject GetContentRecipePrefab(HashSet<RecipeIngredient> contentIngredients)
    {
        foreach (RecipeScript recipe in allRecipes)
        {
            if (recipe.ingredients.SetEquals(contentIngredients)) return recipe.data.towerPrefab;
        }
        return null;
    }
}
