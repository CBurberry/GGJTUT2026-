using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScereTranslateManager scoreTranslateManager;

    [Header("--- §ŒÀŠÔ‚Æ‹’®—¦ ---")]
    public float targetTime = 60f;     // yC³z–¼‘O‚ğ•ª‚©‚è‚â‚·‚­•ÏX
    public float currentRating = 50f;
    private float elapsedTime = 0f;    // y’Ç‰Áz0‚©‚ç”‚¦‚é‚½‚ß‚Ì•Ï”

    [Header("--- UIİ’è ---")]
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;

    private bool isFinished = false;

    void Start()
    {
        Time.timeScale = 1f; // ”O‚Ì‚½‚ß’Ç‰Á
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameClearPanel != null) gameClearPanel.SetActive(false);
        if (timeText != null) timeText.gameObject.SetActive(true);
        UpdateTimerUI();
    }

    void Update()
    {
        if (isFinished) return;

        // yC³zŠÔ‚ğ‘«‚µ‚Ä‚¢‚­
        elapsedTime += Time.deltaTime;

        UpdateTimerUI();

        if (currentRating <= 0) GameOver();
        else if (elapsedTime >= targetTime) GameClear(); // yC³z–Ú•WŠÔ‚É’B‚µ‚½‚çƒNƒŠƒA
    }

    void UpdateTimerUI()
    {
        if (timeText != null)
        {
            // yC³z•\¦ƒƒWƒbƒN‚Ì•ÏX
            if (elapsedTime >= targetTime)
            {
                timeText.text = "10:00"; // I—¹‚ÍŒÅ’è
            }
            else
            {
                int seconds = Mathf.FloorToInt(elapsedTime % 60f);
                timeText.text = string.Format("9:{0:D2}", seconds); // 9:XX Œ`®
            }
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

        

        if (gameClearPanel != null) gameClearPanel.SetActive(true);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}