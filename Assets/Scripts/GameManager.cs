using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    [Header("制限時間と視聴率 ")]
    public float timeLimit = 60f;      // 制限時間
    public float currentRating = 50f;  // 現在の視聴率

    [Header("UI設定")]
    public GameObject gameOverPanel;   
    public GameObject gameClearPanel;  

    private bool isFinished = false;   

    void Start()
    {
      
        Time.timeScale = 1f;

      
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameClearPanel != null) gameClearPanel.SetActive(false);
    }

    void Update()
    {
        
        if (isFinished) return;

        //  カウントダウン
        timeLimit -= Time.deltaTime;

      
        
        if (currentRating <= 0)
        {
            GameOver();
        }
        else if (timeLimit <= 0)
        {
            GameClear();
        }
    }

    // ゲームオーバー
    void GameOver()
    {
        isFinished = true;
        currentRating = 0; 

        // 時間を止める
        Time.timeScale = 0f;

        // パネルを表示
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Debug.Log("【判定】ゲームオーバー：放送事故が発生しました。");
    }

  
    void GameClear()
    {
        isFinished = true;

        //時間を止める
        Time.timeScale = 0f;

        // スコア（視聴率）を保存
        PlayerPrefs.SetFloat("LatestRating", currentRating);
        PlayerPrefs.Save(); 

        // クリアパネルを表示
        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
        }

        Debug.Log("【判定】クリア！ 視聴率 " + currentRating + "% を保存 ");
    }

    // リトライ
    public void RetryGame()
    {
        
        Time.timeScale = 1f;

        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    // ボタン
    public void GoToNextScene(string sceneName)
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}