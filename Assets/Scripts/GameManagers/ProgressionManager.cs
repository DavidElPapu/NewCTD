using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;
    private HashSet<int> unlockedIngredientsIDs;
    private int unlockedLevels, completedLevels, currentMoney;
    public HashSet<int> UnlockedIngredientsIDs => unlockedIngredientsIDs;
    public int UnlockedLevels => unlockedLevels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
            Destroy(gameObject);
    }

    private void Initialize()
    {
        unlockedIngredientsIDs = new HashSet<int>();
        unlockedLevels = 1;
        completedLevels = 0;
        currentMoney = 0;
    }
}
