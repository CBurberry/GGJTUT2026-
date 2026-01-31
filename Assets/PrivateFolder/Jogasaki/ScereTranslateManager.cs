using Unity.Burst;
using UnityEngine;


public class ScereTranslateManager : MonoBehaviour
{
    [SerializeField] private BroadcastAreaManager broadcastAreaManager;
    [SerializeField] private SlliderArea slliderArea;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float easingSpeed = 5f;
    public float score;
    private float velocity;          // SmoothDampóp

    void Start()
    {
    }
    
 
    void Update()
    {
        float targetScore = 50f + broadcastAreaManager.score / 100f;

        float smoothTime = 0.25f;
        float next = Mathf.SmoothDamp(score, targetScore, ref velocity, smoothTime);

        // 70à»è„ÇÕè„è∏ó Çêßå¿
        if (score >= 70f)
        {
            // 1ïbÇ…10ÇµÇ©è„Ç™ÇÁÇ»Ç¢
            float maxDelta = 5f * Time.deltaTime; 
            next = Mathf.Min(next, score + maxDelta);
        }
        if (score <= 30f && next < score)
        {
            float maxDownDelta = 5f * Time.deltaTime; // 1ïbÇ…5ÇµÇ©â∫Ç™ÇÁÇ»Ç¢
            next = Mathf.Max(next, score - maxDownDelta);
        }
        score = next;
        slliderArea.Score_Slider_controller(score);
    }

}
