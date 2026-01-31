using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class SlliderArea : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float addValue = 10.0f;
    [SerializeField] private Image fillImage;
    private void Start()
    {
        if (fillImage == null)
        {
            fillImage = slider.fillRect.GetComponent<Image>();
        }
    }
    private void Update()
    {
        ChangeSliderColor();
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
    private void ChangeSliderColor()
    {

        if (slider.value <= 15.0f)
        {
            fillImage.color = Color.red;
        }
        else if (slider.value <= 40.0f)
        {
            fillImage.color = Color.yellow;
        }
        else if(slider.value <=50.0f)
        {
            fillImage.color = Color.green;
        }
        else if (slider.value >= 70.0f)
        {
            fillImage.color = Color.blue;
        }
    }

    public void Score_Slider_controller(float score)
    {
        slider.value = score;
    }
}
