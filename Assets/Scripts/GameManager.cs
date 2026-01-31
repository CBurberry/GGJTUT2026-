using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScereTranslateManager scoreTranslateManager;

    [Header("--- 制限時間と視聴率 ---")]
    public float timeLimit = 60f;
    public float currentRating = 50f;

    [Header("--- UI設定 ---")]
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public TextMeshProUGUI timeText;   
    public TextMeshProUGUI scoreText;  

    private bool isFinished = false;

    void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameClearPanel != null) gameClearPanel.SetActive(false);
        if (timeText != null) timeText.gameObject.SetActive(true);
        UpdateTimerUI();
    }

    void Update()
    {
        if (isFinished) return;

        timeLimit -= Time.deltaTime;
        //currentRating -= Time.deltaTime; // テスト用
        UpdateTimerUI();

        if (currentRating <= 0) GameOver();
        else if (timeLimit <= 0) GameClear();
    }

    void UpdateTimerUI()
    {
        if (timeText != null)
        {
            timeText.text = Mathf.CeilToInt(Mathf.Max(0, timeLimit)).ToString();
        }
    }

    void GameOver()
    {
        if (isFinished) return;
        isFinished = true;
        Time.timeScale = 0f;
        if (timeText != null) timeText.gameObject.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void GameClear()
    {
        if (isFinished) return;
        isFinished = true;
        Time.timeScale = 0f;

        PlayerPrefs.SetFloat("LatestRating", scoreTranslateManager.score);
        PlayerPrefs.Save();

        if (scoreText != null)
        {
            scoreText.text = scoreTranslateManager.score.ToString("F1") + " %";
        }

        if (timeText != null) timeText.gameObject.SetActive(false);
        if (gameClearPanel != null) gameClearPanel.SetActive(true);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}