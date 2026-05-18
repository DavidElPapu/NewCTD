using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ItemHolder))]
public class TimeProcessor : MonoBehaviour
{
    [Header("For Ingredient Processing")]
    [SerializeField] private Mesh processedMesh;
    [SerializeField] private Vector3 customSize, customOffset;
    [SerializeField] private int maxProcessMeter;
    private int currentProcessMeter;
    [Header("For Ingredient or Container Processing")]
    [SerializeField] private IngredientState processedState;
    [SerializeField] private float processMeterValueTimeFrequency;
    [SerializeField] private int processMeterValue;
    private ItemHolder itemHolder;
    private Coroutine timerProcess;

    private void Awake()
    {
        TryGetComponent(out itemHolder);
        timerProcess = null;
        currentProcessMeter = 0;
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
        if (timerProcess == null)
        {
            timerProcess = StartCoroutine(Timer(item));
        }
    }

    private void StopProcessing(GameObject item)
    {
        if (timerProcess != null)
        {
            StopCoroutine(timerProcess);
            timerProcess = null;
        }
    }

    private IEnumerator Timer(GameObject item)
    {
        if (item.TryGetComponent(out ProcessContainerScript processContainer))
        {
            while (!processContainer.IsProcessDone())
            {
                processContainer.AddProcessMeter(processMeterValue);
                yield return new WaitForSeconds(processMeterValueTimeFrequency);
            }
            processContainer.OnProcessDone(processedState);
        }
        else if (item.TryGetComponent(out IngredientScript ingredient))
        {
            currentProcessMeter = 0;
            while (currentProcessMeter < maxProcessMeter)
            {
                currentProcessMeter += processMeterValue;
                yield return new WaitForSeconds(processMeterValueTimeFrequency);
            }
            ingredient.state = processedState;
            ingredient.ChangeMesh(processedMesh);
            ingredient.ChangeSizeAndOffset(customSize, customOffset);
        }
        timerProcess = null;
        yield break;
    }
}
