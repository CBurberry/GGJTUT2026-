using UnityEngine;

public class ScereTranslateManager : MonoBehaviour
{
    [SerializeField] private BroadcastAreaManager broadcastAreaManager;
    [SerializeField] private SlliderArea slliderArea;
    [SerializeField] private GameManager gameManager;

    public float score;


    void Start()
    {
        
    }

    void Update()
    {
        score = 50 + broadcastAreaManager.score / 200;
        slliderArea.Score_Slider_controller(score);
    }

}
