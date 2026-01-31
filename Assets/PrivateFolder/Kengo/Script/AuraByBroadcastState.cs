using UnityEngine;

public class AuraByBroadcastState : MonoBehaviour
{
    [Header("子オブジェクト名（赤オーラ）")]
    public string badAuraObjectName = "BadAura";

    private ObjClickJudge judge;
    private GameObject badAura;

    void Awake()
    {
        // 自分自身の ObjClickJudge を取得
        judge = GetComponent<ObjClickJudge>();
        if (judge == null)
        {
            Debug.LogWarning($"{name} に ObjClickJudge がありません");
            return;
        }

        // 子オブジェクトから BadAura を自動取得
        Transform auraTransform = transform.Find(badAuraObjectName);
        if (auraTransform == null)
        {
            Debug.LogWarning($"{name} に {badAuraObjectName} が見つかりません");
            return;
        }

        badAura = auraTransform.gameObject;

        UpdateAura();
    }

    void Update()
    {
        UpdateAura();
    }

    void UpdateAura()
    {
        if (badAura == null || judge == null) return;

        // false（悪い放送）なら赤オーラON
        badAura.SetActive(!judge.IsGoodBroadcasting);
    }
}
