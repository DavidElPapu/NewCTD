using System.Collections.Generic;
using UnityEngine;

public class RecipeScript
{
    //The point of this class is just to change the list of ingredients to a hashset since the hashset can't be serialized
    public RecipeSO data;
    public HashSet<RecipeIngredient> ingredients;

    public RecipeScript(RecipeSO data, List<RecipeIngredient> recipeIngredients)
    {
        this.data = data;
        ingredients = new HashSet<RecipeIngredient>(recipeIngredients);
    }
}