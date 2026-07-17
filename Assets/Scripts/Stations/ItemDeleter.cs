using System.Collections.Generic;
using UnityEngine;

public class ItemDeleter : MonoBehaviour, IPlaceable
{
    public bool CanPlaceItem(CookingObject item)
    {
        if (item is IngredientScript ingredient)
        {
            Destroy(item.gameObject);
            return true;
        }
        else if (item is ContainerScript container && container.GetContainedIngredients() != null)
        {
            List<IngredientScript> transferIngredients = new List<IngredientScript>(container.GetContainedIngredients());
            container.EmptyContainer();
            foreach (IngredientScript ingredient2 in transferIngredients)
            {
                Destroy(ingredient2.gameObject);
            }
        }
        return false;
    }

    public CookingObject OnPickEmpty()
    {
        return null;
    }
}
