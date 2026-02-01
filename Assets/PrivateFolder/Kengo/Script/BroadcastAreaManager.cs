using UnityEngine;
using TMPro;

public class BroadcastAreaManager : MonoBehaviour
{
    [Header("UI枠")]
    public RectTransform areaRect;

    [Header("対象3Dオブジェクト")]
    public ObjClickJudge[] targets;


    [Header("増減量")]
    public int deltaPerFrame = 1;

    public int score = 0;

    // ほかのスクリプトでしっかり参照する数値
    public int insideCount = 0;
    public int plusCount = 0;
    public int minusCount = 0;

    void Start()
    {
        if (areaRect == null) Debug.LogError("areaRect が未設定！");

    }

    void Update()
    {

        // カウントリセット
        insideCount = 0;
        plusCount = 0;
        minusCount = 0;

        ObjClickJudge[] allTargets =
            GameObject.FindObjectsByType<ObjClickJudge>(FindObjectsSortMode.None);

        foreach (var target in allTargets)
        {
            if (target == null) continue;

            Renderer rend = target.GetComponent<Renderer>();
            if (rend == null) continue;

            if (IsRendererInsideUI(rend))
            {
                insideCount++;

                if (!target.IsGoodBroadcasting)
                    minusCount++;  // 青 → 減点
                else
                    plusCount++;   // 赤 → 加点
            }
        }

        // ★ 計算式のみ変更（1フレームでの過剰増減を防ぐ）
        score += plusCount*3;
        score -= minusCount*6;

    }

    // ★ 見た目どおりに判定する（遠距離対応）
    private bool IsRendererInsideUI(Renderer rend)
    {
        Camera cam = Camera.main;
        if (cam == null) return false;

        Bounds b = rend.bounds;

        // ① 中心点（遠くて小さい物用）
        if (IsInsideUI(b.center))
            return true;

        // ② boundsの8頂点（近くて大きい物用）
        Vector3[] points =
        {
            b.min,
            b.max,
            new Vector3(b.min.x, b.min.y, b.max.z),
            new Vector3(b.min.x, b.max.y, b.min.z),
            new Vector3(b.max.x, b.min.y, b.min.z),
            new Vector3(b.min.x, b.max.y, b.max.z),
            new Vector3(b.max.x, b.min.y, b.max.z),
            new Vector3(b.max.x, b.max.y, b.min.z)
        };

        foreach (var p in points)
        {
            if (IsInsideUI(p))
                return true;
        }

        return false;
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

}
