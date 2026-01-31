// ClickToShowMask.cs
using UnityEngine;

public class ClickToShowMask : MonoBehaviour
{
    [Header("表示させたい子オブジェクト")]
    public GameObject childToShow;

    private ObjClickJudge clickJudge;

   
    public void ShowChild()
    {
        if (childToShow != null)
        {
            childToShow.SetActive(true);
            Debug.Log($"{name}: 子オブジェクトを表示しました");
        }
    }

    void OnDestroy()
    {
        if (clickJudge != null)
        {
            clickJudge.OnClicked -= ShowChild;
        }
    }
}
