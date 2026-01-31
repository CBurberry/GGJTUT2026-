using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueText:MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text valueText;

    void Update()
    {
        valueText.text = slider.value.ToString("0"); 
    }



}
