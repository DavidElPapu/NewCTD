using UnityEngine;
using System.Collections;

public class TimeContainerProcessor : MonoBehaviour, IHoldProcessor
{
    [SerializeField] private IngredientState processedState;
    [SerializeField] private float processMeterValueTimeFrequency;
    [SerializeField] private int processMeterValue;
    private Coroutine timerProcess;

    private void Awake()
    {
        timerProcess = null;
    }

    public void OnItemEnter(CookingObject item)
    {
        if (timerProcess == null)
        {
            timerProcess = StartCoroutine(Timer(item));
        }
    }

    public void OnItemExit(CookingObject item)
    {
        if (timerProcess != null)
        {
            StopCoroutine(timerProcess);
            timerProcess = null;
        }
    }

    private IEnumerator Timer(CookingObject item)
    {
        if (item is ProcessContainerScript processContainer)
        {
            while (!processContainer.IsProcessDone())
            {
                processContainer.AddProcessMeter(processMeterValue);
                yield return new WaitForSeconds(processMeterValueTimeFrequency);
            }
            processContainer.OnProcessDone(processedState);
        }
        timerProcess = null;
        yield break;
    }
}
