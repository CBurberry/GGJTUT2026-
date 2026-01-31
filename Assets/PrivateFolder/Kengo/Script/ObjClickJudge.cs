using UnityEngine;
using System;

public class ObjClickJudge : MonoBehaviour
{
    // true = 良い放送 / false = 悪い放送
    public bool IsGoodBroadcasting { get; private set; } = false;

    [Header("初期状態設定")]
    [Tooltip("ONならランダム / OFFなら手動指定")]
    public bool useRandomStart = false;

    [Tooltip("useRandomStart が OFF のときに使用（true=良い / false=悪い）")]
    public bool startAsGood = false;

    // 一度だけクリック可能
    public bool clicked = false;

    public event Action OnClicked;

    private ClickToShowMask showMask;

    void Awake()
    {
        // ★ 初期状態決定
        if (useRandomStart)
        {
            IsGoodBroadcasting = UnityEngine.Random.value > 0.5f;
        }
        else
        {
            IsGoodBroadcasting = startAsGood;
        }

        Debug.Log($"{name} Awake: IsGoodBroadcasting = {IsGoodBroadcasting} (Random={useRandomStart})");

        showMask = GetComponent<ClickToShowMask>();
    }

    void OnMouseDown()
    {
        if (clicked) return;

        // 状態反転
        IsGoodBroadcasting = !IsGoodBroadcasting;
        clicked = true;

        Debug.Log($"{name} Clicked: IsGoodBroadcasting = {IsGoodBroadcasting}");

        showMask.ShowChild();
        OnClicked?.Invoke();
    }
}
