using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton;
    public SetupSelectionUI setupSelectionUI { get; private set; }
    public IngredientSelectionUI ingredientSelectionUI { get; private set; }
    public PlayerUI playerUI { get; private set; }
    public WaveUI waveUI { get; private set; }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            setupSelectionUI = GetComponentInChildren<SetupSelectionUI>(true);
            ingredientSelectionUI = GetComponentInChildren<IngredientSelectionUI>(true);
            playerUI = GetComponentInChildren<PlayerUI>(true);
            waveUI = GetComponentInChildren<WaveUI>(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
