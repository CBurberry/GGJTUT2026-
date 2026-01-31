using UnityEngine;
using System;

public class ObjClickJudge : MonoBehaviour
{
    private Renderer rend;

    // 外部から参照できるフラグ
    public bool IsClicked { get; private set; } = false;

    // 一度だけクリック可能にするフラグ
    public bool clicked = false;

    // クリック時に通知するイベント
    public event Action OnClicked;

    private ClickToShowMask showMask;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        // 修正後
        IsClicked = UnityEngine.Random.value > 0.8f;
        rend.material.color = IsClicked ? Color.red : Color.blue;

        Debug.Log($"{name} Awake: 初期色 = {(IsClicked ? "赤" : "青")}");

        showMask = GetComponent<ClickToShowMask>();
    }

    void OnMouseDown()
    {
        if (clicked)
        {
            Debug.Log($"{name} OnMouseDown: すでにクリック済み");
            return;
        }



        IsClicked = !IsClicked;
        rend.material.color = IsClicked ? Color.red : Color.blue;
        clicked = true;

        Debug.Log($"{name} OnMouseDown: クリックされました。IsClicked = {IsClicked}, clicked = {clicked}");

        // クリックされたことを通知
        showMask.ShowChild();
    }
}
