using UnityEngine;
using UnityEngine.UI;

public class ProcessMeterUI : MonoBehaviour
{
    [SerializeField] private Slider processMeterSlider;
    [SerializeField] private Image readyIcon;

    //public void ToggleProcessMeterSliderVisibility(bool showSlider)
    //{
    //    if (showSlider)
    //        processMeterSlider.gameObject.SetActive(true);
    //    else
    //        processMeterSlider.gameObject.SetActive(false);
    //}

    public void UpdateProcessMeterSlider(int newCurrentValue, int maxValue)
    {
        bool showSlider = newCurrentValue > 0 && newCurrentValue < maxValue;
        bool showReadyIcon = newCurrentValue >= maxValue && newCurrentValue != 0;

        if (processMeterSlider.maxValue != maxValue)
            processMeterSlider.maxValue = maxValue;

        processMeterSlider.value = newCurrentValue;

        //These are security checks to make sure it shows what it should show
        if (processMeterSlider.gameObject.activeSelf != showSlider)
            processMeterSlider.gameObject.SetActive(showSlider);
        if (readyIcon.gameObject.activeSelf != showReadyIcon)
            readyIcon.gameObject.SetActive(showReadyIcon);
    }

    //public void SetProcessMeterSliderMaxValue(int newMaxValue)
    //{
    //    processMeterSlider.maxValue = newMaxValue;
    //}
}
