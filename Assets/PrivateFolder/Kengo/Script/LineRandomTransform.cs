using UnityEngine;

public class LineRandomTransform : MonoBehaviour
{
    [Header("移動範囲の端点座標")]
    public Vector3 pointA;
    public Vector3 pointB;

    [Header("ランダム移動設定")]
    public float moveInterval = 1f;  // 移動間隔（秒）

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= moveInterval)
        {
            timer = 0f;
            MoveRandom();
        }
    }

    void MoveRandom()
    {
        // ランダムな位置（0~1の比率）を取得
        float t = Random.Range(0f, 1f);
        // 線形補間で移動
        transform.position = Vector3.Lerp(pointA, pointB, t);
    }

    // Gizmosで線を可視化
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointA, pointB);
    }
}
