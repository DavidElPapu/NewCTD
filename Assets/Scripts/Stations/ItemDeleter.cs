using System.Collections.Generic;
using UnityEngine;

public class ItemDeleter : MonoBehaviour, IPlaceable
{
    public bool CanPlaceItem(GameObject item)
    {
        if (item.TryGetComponent(out IngredientScript ingredient))
        {
            Destroy(item);
            return true;
        }
        else if (item.TryGetComponent(out ContainerScript container) && container.GetContainedIngredients() != null)
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

    public GameObject OnPickEmpty()
    {
        return null;
    }
}
