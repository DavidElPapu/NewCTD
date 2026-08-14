using System;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{
    public event Action SelectionDone;

    public void StartSelection()
    {
        SelectionDone?.Invoke();
    }
}
