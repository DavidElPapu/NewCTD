using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private GameObject waveStatusPanel;
    [SerializeField] private Slider waveTimerSlider, baseHealthSlider;
    [SerializeField] private TextMeshProUGUI mainWaveText, secondaryWaveText;

    public void ShowWaveUI(float maxBaseHealth)
    {
        waveStatusPanel.SetActive(true);
        baseHealthSlider.maxValue = maxBaseHealth;
        baseHealthSlider.value = maxBaseHealth;
        baseHealthSlider.gameObject.SetActive(true);
    }

    public void HideWaveUI()
    {
        waveStatusPanel.SetActive(false);
    }

    public void ResetWaveTimerSlider(float maxWaveTime)
    {
        waveTimerSlider.maxValue = maxWaveTime;
        waveTimerSlider.value = 0f;
    }

    public void UpdateWaveTimerSlider(float newWaveTime)
    {
        waveTimerSlider.value = newWaveTime;
    }

    public void DisplayCurrentWaveText(int currentWave, int maxWave)
    {
        if (currentWave >= maxWave)
            mainWaveText.text = "Final Wave!";
        else
            mainWaveText.text = "Wave: " + currentWave;
    }

    public void DisplayEnemiesLeftText(int enemiesLeft)
    {
        secondaryWaveText.text = "Enemies left: " + enemiesLeft;
    }

    public void DisplayBreakText(int currentWave, int maxWaves)
    {
        mainWaveText.text = "Prepare for next Wave!";
        secondaryWaveText.text = $"Waves: {currentWave}/{maxWaves}";
    }

    public void UpdateBaseHealthSlider(float newBaseHealth)
    {
        baseHealthSlider.value = newBaseHealth;
    }
}
