using UnityEngine;
using System.Collections.Generic;

public class ItemHolder : MonoBehaviour, IPlaceable
{
    [SerializeField] private Transform itemLocation;
    [SerializeField] private List<ContainerName> validContainers;
    private GameObject itemHolded;

    private void Awake()
    {
        itemHolded = null;
    }

    public bool CanPlaceItem(GameObject item)
    {
        if(item.TryGetComponent(out ICookingObject cookingObject))
        {
            if (itemHolded != null)
            {
                if (itemHolded.TryGetComponent(out ICookingObject myCookingObject) && myCookingObject.GetCookingObjectType() == CookingObjectType.Container)
                {
                    if (itemHolded.TryGetComponent(out ContainerScript myContainer))
                    {
                        if (cookingObject.GetCookingObjectType() == CookingObjectType.Container && item.TryGetComponent(out ContainerScript container))
                        {
                            //Falta checar si el container puede dropear, si si, entonces recibir su lista de ingredientes y ver si pueden entrar en mi contenedor
                        }
                        else if (cookingObject.GetCookingObjectType() == CookingObjectType.Ingredient && item.TryGetComponent(out IngredientScript aloneIngredient))
                        {
                            if (myContainer.CanPlaceIngredient(aloneIngredient))
                            {
                                //Falta que ponga el ingrediente en el container.
                                return true;
                            }
                        }
                    }
                }
                //For now, you can't mix ingredients outside a container
            }
            else
            {
                if (cookingObject.GetCookingObjectType() == CookingObjectType.Container && item.TryGetComponent(out ContainerScript container))
                    if (!IsContainerValid(container.cName)) return false;
                item.transform.parent = null;
                item.transform.position = itemLocation.position;
                item.transform.rotation = itemLocation.rotation;
                itemHolded = item;
                return true;
            }
        }
        return false;
    }

    public GameObject OnPickEmpty()
    {
        if (itemHolded == null) return null;
        GameObject tempHold = itemHolded;
        itemHolded = null;
        return tempHold;
    }

    private bool IsContainerValid(ContainerName container)
    {
        if (validContainers.Count == 0) return true;
        foreach (ContainerName validContainer in validContainers)
        {
            if (container == validContainer) return true;
        }
        return false;
    }
}
