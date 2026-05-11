using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ItemHolder))]
public class TimeProcessor : MonoBehaviour
{
    [SerializeField] private IngredientState processedState;
    [SerializeField] private float processTimeFrequency;
    [SerializeField] private int processMeterValue;
    private ItemHolder itemHolder;
    private Coroutine timerProcess;

    private void Awake()
    {
        TryGetComponent(out itemHolder);
        timerProcess = null;
    }

    private void OnEnable()
    {
        itemHolder.OnItemHold += StartProcessing;
        itemHolder.OnItemLeave += StopProcessing;
    }

    private void OnDisable()
    {
        itemHolder.OnItemHold -= StartProcessing;
        itemHolder.OnItemLeave -= StopProcessing;
    }

    private void StartProcessing(GameObject item)
    {
        //For now it can only process ingredients through containers not directly
        if (item.TryGetComponent(out ICookingObject cookingObject) && cookingObject.GetCookingObjectType() == CookingObjectType.Container)
        {
            if (timerProcess == null && item.TryGetComponent(out ProcessContainerScript processContainer))
            {
                timerProcess = StartCoroutine(Timer(processContainer));
            }
        }
    }

    private void StopProcessing(GameObject item)
    {
        //Checking if its a container may be redundant so this line might be deleted
        if (item.TryGetComponent(out ICookingObject cookingObject) && cookingObject.GetCookingObjectType() == CookingObjectType.Container)
        {
            if (timerProcess != null)
            {
                StopCoroutine(timerProcess);
                timerProcess = null;
            }
        }
    }

    private IEnumerator Timer(ProcessContainerScript container)
    {
        while (!container.IsProcessDone())
        {
            container.AddProcessMeter(processMeterValue);
            yield return new WaitForSeconds(processTimeFrequency);
        }
        container.OnProcessDone(processedState);
        timerProcess = null;
        yield break;
    }
}
