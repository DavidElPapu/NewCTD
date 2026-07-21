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

    [Header("UI")]
    [SerializeField] protected WorldspaceUIControler uiControler;
    private ProcessMeterUI processUI;

    protected override void Awake()
    {
        base.Awake();
        timerProcess = null;
        //If this station processes ingredients, it needs its own process meter (UI), so only if it was assigned in inspector, it gets initialized
        if (uiControler != null)
        {
            uiControler.TryGetComponent(out processUI);
            processUI.UpdateProcessMeterSlider(0, 0);
            uiControler.RotateUIToCamera();
        }
    }

    protected override void UpdateItemChange()
    {
        if (timerProcess == null && itemHeld != null)
        {
            //If there is an item but no active timer, checks if its a container that is done, if not, starts timer
            if (itemHeld is ProcessContainerScript container && container.IsProcessDone()) return;
            timerProcess = StartCoroutine(Timer(itemHeld));
        }
        else if (itemHeld == null)
        {
            if (timerProcess != null)
            {
                StopCoroutine(timerProcess);
                timerProcess = null;
            }
            if (uiControler != null)
            {
                processUI.UpdateProcessMeterSlider(0, 0);
            }
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
                    processUI.UpdateProcessMeterSlider(currentProcessMeter, IngredientScript.ingredientProcessMeter);
                }
                yield return null;
            }
            ingredient.state = processedState;
            ingredient.ChangeMesh(processedMesh);
            processUI.UpdateProcessMeterSlider(currentProcessMeter, IngredientScript.ingredientProcessMeter);
        }
        timerProcess = null;
    }
}
