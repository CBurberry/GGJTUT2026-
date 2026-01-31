using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera が見つかりません");
        }
    }

    void Update()
    {
        if (mainCamera == null) return;

        // カメラ方向ベクトル（ワールド座標）
        Vector3 dir = mainCamera.transform.position - transform.position;

        // 上下を固定したい場合
        dir.y = 0;

        if (dir.sqrMagnitude > 0.001f)
        {
            // 親の回転に影響されず、ワールド座標で向く
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(0, 180f, 0);

            //正面を向く
            //transform.rotation = Quaternion.Euler(0, 0f, 0);

        }
    }
}
