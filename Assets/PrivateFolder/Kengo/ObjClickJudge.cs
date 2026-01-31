using UnityEngine;

public class ObjClickJudge : MonoBehaviour
{
    private Renderer rend;

    // 外部から参照できるフラグ
    public bool IsClicked { get; private set; } = false;

    // 一度だけクリック可能
    private bool clicked = false;

    void Awake()
    {
        rend = GetComponent<Renderer>();

        // 初期状態をランダムに設定
        IsClicked = Random.value > 0.5f; // 50%の確率で赤か青
        rend.material.color = IsClicked ? Color.red : Color.blue;
    }

    void OnMouseDown()
    {
        // すでにクリック済みなら何もしない
        if (clicked) return;

        // 赤なら青、青なら赤に切り替え
        IsClicked = !IsClicked;
        rend.material.color = IsClicked ? Color.red : Color.blue;

        // 一度クリックしたら二度と変えられない
        clicked = true;

    }
}
