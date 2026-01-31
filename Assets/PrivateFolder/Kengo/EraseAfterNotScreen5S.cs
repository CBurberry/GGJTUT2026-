using UnityEngine;

public class EraseAfterNotScreen5S : MonoBehaviour
{
    [Header("画面外で削除までの時間（秒）")]
    public float timeToErase = 5f;

    private float offScreenTimer = 0f;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("Main Camera が見つかりません！");
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
                Debug.Log($"{gameObject.name} を削除（画面外5秒以上）");
            }
        }
    }

    // オブジェクトが画面内に映っているか判定
    private bool IsOnScreen()
    {
        Vector3 screenPos = mainCam.WorldToViewportPoint(transform.position);

        // screenPos.z > 0 はカメラの前にあるか
        // x,y が 0～1 の範囲なら画面内
        return screenPos.z > 0 && screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1;
    }
}
