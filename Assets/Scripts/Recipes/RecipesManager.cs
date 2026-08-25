using System.Collections.Generic;
using UnityEngine;
using System;

public class RecipesManager : MonoBehaviour
{
    public event Action LevelRecipesSet;
    [SerializeField] private List<RecipeSO> allRecipesSO;
    private List<RecipeScript> allRecipes, levelRecipes;

    private void Awake()
    {
        SetAllRecipes();
    }

    private void SetAllRecipes()
    {
        allRecipes = new List<RecipeScript>();
        foreach (RecipeSO recipeSO in allRecipesSO)
        {
            allRecipes.Add(new RecipeScript(recipeSO, recipeSO.requiredIngredients));
        }
    }

    public void SetLevelRecipes()
    {
        //This method filters all possible recipes in the level based on the selected ingredients and map stations and objects
        //Initializes level recipes with all recipes
        levelRecipes = new List<RecipeScript>(allRecipes);

        //Loops through all stations and items in the level to get all possible ingredients and states combinations
        HashSet<CookingObjectName> possibleIngredientsInLevel = new HashSet<CookingObjectName>();
        HashSet<IngredientState> possibleStatesInLevel = new HashSet<IngredientState>();

        //For now this is done just for the ice ingredient
        possibleStatesInLevel.Add(IngredientState.ReadyToDrink);

        foreach (StationScript station in LevelManager.singleton.mapManager.levelStations)
        {
            if (station is ItemProvider itemProvider)
                possibleIngredientsInLevel.Add(itemProvider.ingredientSO.iName);
            else if (station is TimeProcessor timeProcessor)
                possibleStatesInLevel.Add(timeProcessor.processedState);
            else if (station is StepProcessor stepProcessor)
            {
                if (stepProcessor.internalTool != null)
                    possibleStatesInLevel.Add(stepProcessor.internalTool.processedState);
            }
        }

        foreach (ToolScript tool in LevelManager.singleton.mapManager.levelTools)
        {
            if (tool is ProcessorTool processorTool)
                possibleStatesInLevel.Add(processorTool.processedState);
        }

        //Once all possible ingredients and states are set, combine them to make all possible recipeingredients
        HashSet<RecipeIngredient> possibleRecipeIngredients = new HashSet<RecipeIngredient>();
        foreach (CookingObjectName possibleIngredient in possibleIngredientsInLevel)
        {
            foreach (IngredientState possibleState in possibleStatesInLevel)
            {
                possibleRecipeIngredients.Add(new RecipeIngredient(possibleIngredient, possibleState));
            }
        }

        //Now for each recipe ingredients, ensures all the ingredients are possible in the level, and removes from the list the ones that dont
        for (int i = levelRecipes.Count - 1; i >= 0; i--)
        {
            bool canBeMade = true;
            foreach (RecipeIngredient requiredIngredient in levelRecipes[i].ingredients)
            {
                if (!possibleRecipeIngredients.Contains(requiredIngredient))
                {
                    canBeMade = false;
                    break;
                }
            }
            if (!canBeMade)
                levelRecipes.RemoveAt(i);
        }

        //Sets the done event for the level manager
        LevelRecipesSet?.Invoke();
    }

    public GameObject GetContentRecipePrefab(HashSet<RecipeIngredient> contentIngredients)
    {
        foreach (RecipeScript recipe in levelRecipes)
        {
            if (recipe.ingredients.SetEquals(contentIngredients)) return recipe.data.towerPrefab;
        }
        return null;
    }
}
