using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeLimit = 60f;
    public float currentRating = 50f;
    public GameObject gameOverPanel;

    private bool isFinished = false;

    void Update()
    {
        if (isFinished) return;

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

    void GameOver()
    {
        isFinished = true;
        currentRating = 0;

        
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    void GameClear()
    {
        isFinished = true;
       
        Time.timeScale = 0f;
        Debug.Log("ƒNƒŠƒAI");
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;

        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}