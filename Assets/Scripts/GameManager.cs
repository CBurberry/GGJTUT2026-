using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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


    public int seconds;

    private bool isFinished = false;

    [Header("--- ƒ‰ƒ“ƒN•Êƒpƒlƒ‹ ---")]
    public GameObject rankSPanel;
    public GameObject rankAPanel;
    public GameObject rankBPanel;
    public GameObject rankCPanel;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;


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
                seconds = Mathf.FloorToInt(elapsedTime % 60f);
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

        float score = 0;
        score= scoreTranslateManager.score;
        PlayerPrefs.SetFloat("LatestRating", score);
        PlayerPrefs.Save();

        scoreText.text = score.ToString("F1") + "%";
        if (rankAPanel != null) rankAPanel.SetActive(false);
        if (rankBPanel != null) rankBPanel.SetActive(false);
        if (rankCPanel != null) rankCPanel.SetActive(false);


        if(score>=100f)
        {
            if (rankSPanel != null) rankSPanel.SetActive(true);
            StartCoroutine(playsound(0));
        }

        else if (score >= 80f)
        {
            if (rankAPanel != null) rankAPanel.SetActive(true);
            StartCoroutine(playsound(1));

        }
        else if (score >= 50f)
        {
            if (rankBPanel != null) rankBPanel.SetActive(true);
            StartCoroutine(playsound(2));

        }
        else
        {
            if (rankCPanel != null) rankCPanel.SetActive(true);
            StartCoroutine(playsound(3));
        }


        if (gameClearPanel != null) gameClearPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator playsound(int num)
    {
        SoundManager.Instance.bgmSource.Stop();
        SoundManager.Instance.audioSource.Stop();
        yield return new WaitForSecondsRealtime(0.5f);
        SoundManager.Instance.PlaySE(audioSource, audioClips[num]);
    }
}