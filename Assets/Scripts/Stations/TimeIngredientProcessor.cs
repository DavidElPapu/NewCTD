using UnityEngine;
using System.Collections;

public class TimeIngredientProcessor : MonoBehaviour, IHoldProcessor
{
    [SerializeField] private Mesh processedMesh;
    [SerializeField] private Vector3 customSize, customOffset;
    [SerializeField] private IngredientState processedState;
    [SerializeField] private float processMeterValueTimeFrequency;
    [SerializeField] private int processMeterValue;
    private Coroutine timerProcess;
    private int currentProcessMeter;

    private void Awake()
    {
        timerProcess = null;
        currentProcessMeter = 0;
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
        if (item is IngredientScript ingredient)
        {
            currentProcessMeter = 0;
            while (currentProcessMeter < IngredientScript.ingredientProcessMeter)
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
