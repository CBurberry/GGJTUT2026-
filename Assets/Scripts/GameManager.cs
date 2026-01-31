using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeLimit = 60f;      // 制限時間
    public float currentRating = 50f;  // 視聴率

    private bool isGameActive = true;  

    void Update()
    {
        if (!isGameActive) return; 
        
        timeLimit -= Time.deltaTime;

        //クリア判定
        if (timeLimit <= 0)
        {
            GameClear();
        }

        // ゲームオーバー判定
        if (currentRating <= 0)
        {
            GameOver();
        }
    }

    void GameClear()
    {
        isGameActive = false;
        Debug.Log("ステージクリア！");
       
    }

    void GameOver()
    {
        isGameActive = false;
        Debug.Log("放送事故！ゲームオーバー");
        
    }
}