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
    [SerializeField] private int countFinishValue = 1;

    [Header("Animation Settings")]
    [Range(1f, 5f)]
    [SerializeField] private float maxScale = 2f; // どのくらい大きくするか
    [SerializeField] private float animationDuration = 1.0f; // 演出にかける時間（1秒以内）

    private Coroutine _countdownCoroutine;

    // 撮影開始ボタンから呼び出す関数
    public void OnStartREC()
    {
        // 既に動いている場合は止める（二重起動防止）
        if (_countdownCoroutine != null) StopCoroutine(_countdownCoroutine);

        _countdownCoroutine = StartCoroutine(CountdownRoutine());

    }

    private IEnumerator CountdownRoutine()
    {
        int currentCount = countdownValue;
        StartButton.SetActive(false);
        while (currentCount > 0)
        {
            // テキストの更新
            countText.text = currentCount.ToString();

            // 演出（拡大＆フェードアウト）を開始
            yield return StartCoroutine(AnimateText());

            currentCount--;
        }
        
        countImage.gameObject.SetActive(true);
        countText.text = "";
        countText.gameObject.SetActive(false);

        //撮影処理呼び出し
        yield return new WaitForSeconds(countFinishValue);

        countImage.gameObject.SetActive(false);//スタート消去
        countText.gameObject.SetActive(false);//カウント消去



    }

    private IEnumerator AnimateText()
    {
        float elapsed = 0f;
        Vector3 initialScale = Vector3.one;
        Vector3 targetScale = Vector3.one * maxScale;
        Color originalColor = countText.color;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;

            // 1. サイズの拡大 (Lerpを使用)
            countText.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // 2. 透明度のフェードアウト
            countText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - t);

            yield return null;
        }

        // 次のカウントに備えて色とスケールをリセット
        countText.transform.localScale = initialScale;
        countText.color = originalColor;
    }
}