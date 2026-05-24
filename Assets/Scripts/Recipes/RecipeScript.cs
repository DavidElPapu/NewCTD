using System.Collections.Generic;
using UnityEngine;

public class RecipeScript
{
    public RecipeSO data;
    public HashSet<RecipeIngredient> ingredients;

    public RecipeScript(RecipeSO data, List<RecipeIngredient> recipeIngredients)
    {
        this.data = data;
        ingredients = new HashSet<RecipeIngredient>(recipeIngredients);
    }
}
