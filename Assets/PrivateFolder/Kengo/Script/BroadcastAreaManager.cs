using UnityEngine;
using TMPro;

public class BroadcastAreaManager : MonoBehaviour
{
    [Header("UI枠")]
    public RectTransform areaRect;

    [Header("対象3Dオブジェクト")]
    public ObjClickJudge[] targets;

    [Header("スコア表示")]
    public TextMeshProUGUI scoreText;

    [Header("増減量")]
    public int deltaPerFrame = 1;

    public int score = 0;

    // ↓ Inspectorで確認できるデバッグ用カウント
    [Header("デバッグ用カウント")]
    public int insideCount = 0;
    public int plusCount = 0;
    public int minusCount = 0;

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

        // デバッグカウントリセット
        insideCount = 0;
        plusCount = 0;
        minusCount = 0;

        // targets 配列だけでなく、シーン上の ObjClickJudge を全て取得してループ
        ObjClickJudge[] allTargets = GameObject.FindObjectsByType<ObjClickJudge>(FindObjectsSortMode.None);

        foreach (var target in allTargets)
        {
            if (target == null) continue;

            if (IsInsideUI(target.transform.position))
            {
                insideCount++;

                // ここで赤かどうかをチェック
                if (target.IsClicked)
                {
                    score += deltaPerFrame;
                    plusCount++;
                    //Debug.Log(target.name + " 枠内：赤 → +");
                }
                else
                {
                    score -= deltaPerFrame;
                    minusCount++;
                    //Debug.Log(target.name + " 枠内：赤以外 → -");
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
