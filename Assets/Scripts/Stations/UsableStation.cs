using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ItemHolder))]
public class UsableStation : MonoBehaviour, IInteractable
{
    [SerializeField] private List<CookingObjectName> validTools;
    [SerializeField] private float useCooldown;
    [SerializeField] private int processMeterValue;

    public void OnUse(GameObject item)
    {
        throw new System.NotImplementedException();
    }
}
