using UnityEngine;
using System.Collections;

public class PeriodicPerformanceBehaviour : MonoBehaviour
{
    [SerializeField] private float intervalTime;
    private Coroutine periodicPerformanceCoroutine;

    private void Awake()
    {
        periodicPerformanceCoroutine = null;
    }

    private void Start()
    {
        periodicPerformanceCoroutine = StartCoroutine(PeriodicPerformance());
    }

    private IEnumerator PeriodicPerformance()
    {
        yield break;
    }
}
