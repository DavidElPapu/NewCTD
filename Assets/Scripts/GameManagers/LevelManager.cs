using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public SelectionUI selectionUI;
    public EnemySpawner wavesManager;

    private void Start()
    {
        Debug.Log("Level started, fisrt select ingredients");
        selectionUI.StartSelection();
    }

    private void OnEnable()
    {
        selectionUI.SelectionDone += OnSelectionPhaseEnd;
        wavesManager.WavesCompleted += OnWavesPhaseEnd;
    }

    private void OnDisable()
    {
        selectionUI.SelectionDone -= OnSelectionPhaseEnd;
        wavesManager.WavesCompleted -= OnWavesPhaseEnd;
    }

    private void OnSelectionPhaseEnd()
    {
        Debug.Log("Selection done, game starts now");
        wavesManager.StartEnemyWaves();
    }

    private void OnWavesPhaseEnd()
    {
        Debug.Log("You Win");
    }
}
