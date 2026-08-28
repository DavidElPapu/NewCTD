using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public static LevelManager singleton;
    public GameObject playerGO;
    public MapManager mapManager;
    public PlayerInput gameplayInput;
    public SetupSelectionManager setupSelectionManager;
    public RecipesManager recipesManager;
    public WavesManager wavesManager;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Debug.Log("Building Map, wait");
        mapManager.SetMapStationsAndItems();
    }

    private void OnEnable()
    {
        mapManager.MapSetupDone += OnMapSetupDone;
        setupSelectionManager.SelectionDone += OnSelectionPhaseEnd;
        recipesManager.LevelRecipesSet += OnLevelRecipesSet;
        wavesManager.WavesCompleted += OnWavesPhaseEnd;
        wavesManager.BaseDestroyed += OnBaseDestroyed;
    }

    private void OnDisable()
    {
        mapManager.MapSetupDone -= OnMapSetupDone;
        setupSelectionManager.SelectionDone -= OnSelectionPhaseEnd;
        recipesManager.LevelRecipesSet -= OnLevelRecipesSet;
        wavesManager.WavesCompleted -= OnWavesPhaseEnd;
        wavesManager.BaseDestroyed -= OnBaseDestroyed;
    }

    private void OnMapSetupDone()
    {
        Debug.Log("Map setup done, now select ingredients");
        gameplayInput.SwitchCurrentActionMap("LevelSetup");
        setupSelectionManager.StartSelection();
    }

    private void OnSelectionPhaseEnd()
    {
        Debug.Log("Selection done, now wait for filter recipes");
        recipesManager.SetLevelRecipes();
    }

    private void OnLevelRecipesSet()
    {
        Debug.Log("Recipes done, game starts now");
        gameplayInput.SwitchCurrentActionMap("Player");
        playerGO.SetActive(true);
        wavesManager.StartEnemyWaves();
    }

    private void OnWavesPhaseEnd()
    {
        Debug.Log("You Win");
    }

    private void OnBaseDestroyed()
    {
        Debug.Log("You Lose");
    }
}
