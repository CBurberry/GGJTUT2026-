using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BroadcastColorView : MonoBehaviour
{
    private Renderer rend;
    private ObjClickJudge judge;

    [Header("表示色")]
    public Color goodColor = Color.blue;  // 良い放送は青
    public Color badColor = Color.red;    // 悪い放送は赤

    void Awake()
    {
        rend = GetComponent<Renderer>();
        judge = GetComponent<ObjClickJudge>();

        if (judge == null)
        {
            Debug.LogError($"{name}: ObjClickJudge が見つかりません");
            enabled = false;
            return;
        }

        // マテリアルをインスタンス化して色を安全に変えられるようにする
        rend.material = new Material(rend.material);

        // 初期状態を反映
        UpdateColor();
    }

    void OnEnable()
    {
        if (judge != null)
        {
            judge.OnClicked += UpdateColor;
        }
    }

    void OnDisable()
    {
        if (judge != null)
        {
            judge.OnClicked -= UpdateColor;
        }
    }

    private void UpdateColor()
    {
        if (judge == null || rend == null) return;

        Color targetColor = judge.IsGoodBroadcasting ? goodColor : badColor;
        rend.material.color = targetColor;

        Debug.Log($"{name} UpdateColor: IsGoodBroadcasting={judge.IsGoodBroadcasting}, color={targetColor}");
    }
}
