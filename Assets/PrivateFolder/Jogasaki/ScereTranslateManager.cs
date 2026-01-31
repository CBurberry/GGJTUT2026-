using UnityEngine;

public class ScereTranslateManager : MonoBehaviour
{
    [SerializeField] private BroadcastAreaManager broadcastAreaManager;
    [SerializeField] private SliderArea sliderArea;
    [SerializeField] private GameManager gameManager;

    private float score;


    void Start()
    {
        
    }

    void Update()
    {
        //sliderArea.(broadcastAreaManager.score);
    }
    //gameManager.(broadcastAreaManager.score)

}
