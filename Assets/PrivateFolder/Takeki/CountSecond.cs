using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class CountSecond : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject countImage;
    [SerializeField] private GameObject StartButton;

    [Header("Settings")]
    [SerializeField] private int countdownValue = 5;
    [SerializeField] private float countFinishDisplayTime = 1.0f; // スタート表示の時間

    [Header("Animation Settings")]
    [Range(1f, 5f)]
    [SerializeField] private float maxScale = 2f;
    [SerializeField] private float animationDuration = 1.0f;

    private Coroutine _countdownCoroutine;

    //Sound
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;

    [SerializeField] private AudioSource bgmSource;

    void Start()
    {
        // 時間を止める
        Debug.Log("Start Called");
        Time.timeScale = 0f;
        Debug.Log("Start Called");
    }
    public void OnStartREC()
    {

        if (_countdownCoroutine != null) StopCoroutine(_countdownCoroutine);
        _countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        SoundManager.Instance.PlaySE(audioSource, audioClip[0]);

        int currentCount = countdownValue;
        StartButton.SetActive(false);
        countText.gameObject.SetActive(true);

        while (currentCount > 0)
        {
            countText.text = currentCount.ToString();

            if (currentCount == 3) SoundManager.Instance.PlaySE(audioSource, audioClip[1]);
            else if (currentCount == 2) SoundManager.Instance.PlaySE(audioSource, audioClip[2]);


            // 演出（AnimateTextもunscaledTimeで動くように修正）
            yield return StartCoroutine(AnimateText());

            currentCount--;
        }

        // カウント終了後の「START」画像などの表示
        countImage.gameObject.SetActive(true);
        countText.text = "";
        countText.gameObject.SetActive(false);
        SoundManager.Instance.PlaySE(audioSource, audioClip[3]);
        SoundManager.Instance.PlaySE(audioSource, audioClip[4]);
        SoundManager.Instance.PlayBGM(bgmSource);

        // 指定した秒数（実時間）待機
        yield return new WaitForSecondsRealtime(countFinishDisplayTime);

        // 2. 撮影開始と同時に時間を動かす
        StartREC();
    }

    public void StartREC()
    {
        Time.timeScale = 1f; // 時間を再開
        countImage.gameObject.SetActive(false);
        countText.gameObject.SetActive(false);
        Debug.Log("Recording Started and Time Resumed!");
    }

    private IEnumerator AnimateText()
    {
        float elapsed = 0f;
        Vector3 initialScale = Vector3.one;
        Vector3 targetScale = Vector3.one * maxScale;
        Color originalColor = countText.color;

        // 時間が止まっていても動くように Time.unscaledDeltaTime を使用
        while (elapsed < animationDuration)
        {
            elapsed += Time.unscaledDeltaTime; // ここがポイント
            float t = Mathf.Clamp01(elapsed / animationDuration);

            countText.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            countText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - t);

            yield return null;
        }

        countText.transform.localScale = initialScale;
        countText.color = originalColor;
    }
}