using UnityEngine;
using TMPro; 

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 

    void OnEnable()
    {
        float score = PlayerPrefs.GetFloat("LatestRating", 0f);
        scoreText.text = score.ToString("F1") + " %";
    }
}