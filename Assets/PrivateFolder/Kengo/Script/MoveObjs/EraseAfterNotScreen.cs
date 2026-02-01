using UnityEngine;

public class EraseAfterNotScreen : MonoBehaviour
{
    [Header("画面外で削除までの時間（秒）")]
    public float timeToErase = 3f;

    [Header("画面端バッファ（0～1、値が大きいほど画面外でも少し残る）")]
    public float screenBuffer = 0.05f;

    private float offScreenTimer = 0f;
    private Camera mainCam;
    private Renderer objRenderer;

    void Start()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("Main Camera が見つかりません！");
            enabled = false;
            return;
        }

        objRenderer = GetComponent<Renderer>();
        if (objRenderer == null)
        {
            Debug.LogError("Renderer が見つかりません！");
            enabled = false;
        }
    }

    void Update()
    {
        if (IsOnScreen())
        {
            // 画面内ならタイマーリセット
            offScreenTimer = 0f;
        }
        else
        {
            // 画面外ならタイマー加算
            offScreenTimer += Time.deltaTime;

            if (offScreenTimer >= timeToErase)
            {
                Destroy(gameObject);
            }
        }
    }

    // オブジェクトが画面内に映っているか判定（バウンディングボックス全体）
    private bool IsOnScreen()
    {
        if (objRenderer == null) return false;

        Bounds b = objRenderer.bounds;

        // バウンディングボックスの8頂点をチェック
        Vector3[] points = new Vector3[8];
        points[0] = mainCam.WorldToViewportPoint(b.min);
        points[1] = mainCam.WorldToViewportPoint(new Vector3(b.min.x, b.min.y, b.max.z));
        points[2] = mainCam.WorldToViewportPoint(new Vector3(b.min.x, b.max.y, b.min.z));
        points[3] = mainCam.WorldToViewportPoint(new Vector3(b.min.x, b.max.y, b.max.z));
        points[4] = mainCam.WorldToViewportPoint(new Vector3(b.max.x, b.min.y, b.min.z));
        points[5] = mainCam.WorldToViewportPoint(new Vector3(b.max.x, b.min.y, b.max.z));
        points[6] = mainCam.WorldToViewportPoint(new Vector3(b.max.x, b.max.y, b.min.z));
        points[7] = mainCam.WorldToViewportPoint(b.max);

        foreach (var p in points)
        {
            // z>0はカメラ前、x,yは画面内＋バッファ
            if (p.z > 0 &&
                p.x >= -screenBuffer && p.x <= 1 + screenBuffer &&
                p.y >= -screenBuffer && p.y <= 1 + screenBuffer)
            {
                return true; // 1点でも画面内なら true
            }
        }
        return false;
    }
}

