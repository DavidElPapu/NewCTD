using UnityEngine;
using System.Collections;

public class TimeProcessor : ItemHolderStation
{
    [SerializeField] private IngredientState processedState;
    [SerializeField] private float processMeterValueTimeFrequency;
    [SerializeField] private int processMeterValue;
    private Coroutine timerProcess;
    private float processMeterValueTimer;

    [Header("Only for ingredient processing")]
    [SerializeField] private Mesh processedMesh;
    private int currentProcessMeter;

    protected override void Awake()
    {
        base.Awake();
        timerProcess = null;
    }

    protected override void UpdateItemChange()
    {
        if (timerProcess == null && itemHeld != null)
        {
            //If there is an item but no active timer, checks if its a container that is done, if not, starts timer
            if (itemHeld is ProcessContainerScript container && container.IsProcessDone()) return;
            timerProcess = StartCoroutine(Timer(itemHeld));
        }
        else if (timerProcess != null && itemHeld == null)
        {
            StopCoroutine(timerProcess);
            timerProcess = null;
        }
    }

    private IEnumerator Timer(CookingObject item)
    {
        processMeterValueTimer = 0f;
        if (item is ProcessContainerScript container)
        {
            //If its a container, starts the timer that adds to the container processMeter until the containerProcess is done, then the container handles the state change
            while (!container.IsProcessDone())
            {
                processMeterValueTimer += Time.deltaTime;
                if (processMeterValueTimer >= processMeterValueTimeFrequency)
                {
                    container.AddProcessMeter(processMeterValue);
                    processMeterValueTimer = 0f;
                }
                yield return null;
            }
            container.OnProcessDone(processedState);
        }
        else if (item is IngredientScript ingredient)
        {
            //If its an ingredient, starts a timer that adds to the currentProcessMeter until it reaches the ingredientProcessMeter, then it changes its state
            currentProcessMeter = 0;
            while (currentProcessMeter < IngredientScript.ingredientProcessMeter)
            {
                processMeterValueTimer += Time.deltaTime;
                if (processMeterValueTimer >= processMeterValueTimeFrequency)
                {
                    currentProcessMeter += processMeterValue;
                    processMeterValueTimer = 0f;
                }
                yield return null;
            }
            ingredient.state = processedState;
            ingredient.ChangeMesh(processedMesh);
        }
        timerProcess = null;
    }
}
