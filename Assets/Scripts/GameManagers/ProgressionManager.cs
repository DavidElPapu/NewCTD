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
        unlockedIngredientsIDs.Add(0);
        unlockedIngredientsIDs.Add(1);
        unlockedIngredientsIDs.Add(2);
        unlockedIngredientsIDs.Add(3);
        unlockedIngredientsIDs.Add(4);
        //unlockedIngredientsIDs.Add(5);
        unlockedLevels = 1;
        completedLevels = 0;
        currentMoney = 0;
    }
}
