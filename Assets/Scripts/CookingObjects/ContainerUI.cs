using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    [SerializeField] private Image[] containedIngredientsImages;

    public void UpdateIngredients(List<IngredientScript> ingredients)
    {
        //Iterate to all UI images and put the ingredient icon from the ingredient list, if there are no left ingredients, it just deactivates the leftover images
        for (int i = 0; i < containedIngredientsImages.Length; i++)
        {
            if (i < ingredients.Count)
            {
                containedIngredientsImages[i].sprite = ingredients[i].data.icon;
                containedIngredientsImages[i].gameObject.SetActive(true);
            }
            else
                containedIngredientsImages[i].gameObject.SetActive(false);
        }
    }
}
