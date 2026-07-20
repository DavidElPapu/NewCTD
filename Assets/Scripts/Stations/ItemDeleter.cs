using System.Collections.Generic;
using UnityEngine;

public class ItemDeleter : StationScript
{
    public override bool TryPlaceItem(CookingObject item)
    {
        //If the item is an ingredient it gets deleted directly, if it's a container, deletes  its content if able without deleting the container
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

    public override CookingObject TryGetItem()
    {
        //Not yet
        return null;
    }

    public override void UseStation(CookingObject item)
    {
        //Nothing
    }
}
