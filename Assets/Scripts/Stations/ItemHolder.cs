using UnityEngine;

public class ItemHolder : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform itemLocation;
    public GameObject itemHolded;

    private void Awake()
    {
        itemHolded = null;
    }

    public bool CanPlaceItem(GameObject item)
    {
        if (itemHolded != null)
        {
            //Here should check the item inside
        }
        else
        {
            //For now, only checks it has no item, but later will check item
            return true;
        }
        return false;
    }

    public GameObject OnPickEmpty()
    {
        throw new System.NotImplementedException();
    }

    public void OnUse()
    {
        //Nothing
    }
}
