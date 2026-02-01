using UnityEngine;
using UnityEngine.UI;

public class SlliderArea : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float addValue = 10.0f;

    public Gradient ColorGradient;

    private Image fillImage;

    private void Start()
    {
        if (fillImage == null)
        {
            fillImage = slider.fillRect.GetComponent<Image>();
            ChangeSliderColor();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            slider.value += addValue * Time.deltaTime;
            slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            slider.value -= addValue*Time.deltaTime;
            slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);
        }
    }

    public void ChangeSliderColor()
    {
        fillImage.color = ColorGradient.Evaluate(slider.value / 100f);
    }

    public void Score_Slider_controller(float score)
    {
        slider.value = score;
    }
}
