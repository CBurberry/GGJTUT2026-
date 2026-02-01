using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InputScoreText : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            scoreText.text = "Viewership: " + slider.value.ToString("F1") + "%";
        }
    }
}
