using UnityEngine;
using TMPro;

public class RedObjectAreaScoreMulti : MonoBehaviour
{
    [Header("UI枠")]
    public RectTransform areaRect;

    [Header("対象3Dオブジェクト")]
    public ClickToRed3D[] targets;

    [Header("スコア表示")]
    public TextMeshProUGUI scoreText;

    [Header("増減量")]
    public int deltaPerFrame = 1;

    private int score = 0;

    void Start()
    {
        if (areaRect == null) Debug.LogError("areaRect が未設定！");
        if (targets == null || targets.Length == 0) Debug.LogError("targets が未設定！");
        if (scoreText == null) Debug.LogError("scoreText が未設定！");

        UpdateScoreText();
    }

    void Update()
    {
        if (areaRect == null || scoreText == null) return;

        // targets 配列だけでなく、シーン上の ClickToRed3D を全て取得してループ
        ClickToRed3D[] allTargets = GameObject.FindObjectsOfType<ClickToRed3D>();

        foreach (var target in allTargets)
        {
            if (target == null) continue;

            if (IsInsideUI(target.transform.position))
            {
                // ここで赤かどうかをチェック
                if (target.IsRed)
                {
                    score += deltaPerFrame;
                    Debug.Log(target.name + " 枠内：赤 → +");
                }
                else
                {
                    score -= deltaPerFrame;
                    Debug.Log(target.name + " 枠内：赤以外 → -");
                }
            }
        }

        UpdateScoreText();
    }

    private bool IsInsideUI(Vector3 worldPos)
    {
        Camera cam = Camera.main;
        if (cam == null) return false;

        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
        if (screenPos.z < 0) return false;

        Vector3[] corners = new Vector3[4];
        areaRect.GetWorldCorners(corners);
        Vector2 min = RectTransformUtility.WorldToScreenPoint(null, corners[0]);
        Vector2 max = RectTransformUtility.WorldToScreenPoint(null, corners[2]);

        return screenPos.x >= min.x && screenPos.x <= max.x &&
               screenPos.y >= min.y && screenPos.y <= max.y;
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
