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
        if (newCurrentValue == 0 || newCurrentValue >= maxValue)
        {
            if (processMeterSlider.gameObject.activeSelf)
                processMeterSlider.gameObject.SetActive(false);
            if (newCurrentValue == 0)
            {
                if (readyIcon.gameObject.activeSelf)
                    readyIcon.gameObject.SetActive(false);
            }
            else if (newCurrentValue >= maxValue)
            {
                if (!readyIcon.gameObject.activeSelf)
                    readyIcon.gameObject.SetActive(true);
            }
            return;
        }
        if (processMeterSlider.maxValue != maxValue)
            processMeterSlider.maxValue = maxValue;
        processMeterSlider.value = newCurrentValue;

        //These are security checks to make sure it shows what it should show
        if (!processMeterSlider.gameObject.activeSelf)
            processMeterSlider.gameObject.SetActive(true);
        if (readyIcon.gameObject.activeSelf)
            readyIcon.gameObject.SetActive(false);
    }

    //public void SetProcessMeterSliderMaxValue(int newMaxValue)
    //{
    //    processMeterSlider.maxValue = newMaxValue;
    //}
}
