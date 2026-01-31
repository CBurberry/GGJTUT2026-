using UnityEngine;

public class ScereTranslateManager : MonoBehaviour
{
    [SerializeField] private BroadcastAreaManager broadcastAreaManager;
    [SerializeField] private SliderArea sliderArea;
    [SerializeField] private GameManager gameManager;

    public float score;


    void Start()
    {
        
    }

    void Update()
    {
        score = 50 + broadcastAreaManager.score / 100;
        //sliderArea.(broadcastAreaManager.score);
    }

}
