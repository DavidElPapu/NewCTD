using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableStation : MonoBehaviour
{
    [SerializeField] private List<CookingObjectName> validTools;
    [SerializeField] private float useCooldown;
    private IUseProcessor useProcessor;
    private bool canBeUsed;

    private void Awake()
    {
        TryGetComponent(out useProcessor);
        canBeUsed = true;
    }

    public void OnUse(GameObject item)
    {
        if(IsItemValid(item) && canBeUsed)
        {
            useProcessor.OnProcessorUse(item);
            canBeUsed = false;
            StartCoroutine(Cooldown());
        }
    }

    private bool IsItemValid(GameObject item)
    {
        if (validTools.Count == 0) return true;
        if (item != null && item.TryGetComponent(out ICookingObject cookingObject))
        {
            foreach (CookingObjectName tool in validTools)
            {
                if (cookingObject.GetCookingObjectName() == tool) return true;
            }
        }
        return false;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(useCooldown);
        canBeUsed = true;
        yield break;
    }
}
