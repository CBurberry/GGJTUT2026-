using UnityEngine;

public class ClickToShowMask : MonoBehaviour
{
    private bool clickJudge=false;

    [Header("生成する子オブジェクトPrefab")]
    public GameObject childPrefab;

    [Header("子の生成オフセット")]
    public Vector3 spawnOffset = Vector3.up;


    void Awake()
    {
        if (childPrefab == null)
        {
            Debug.LogError("生成する子オブジェクトPrefabがセットされていません");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("クリックした");
        if (clickJudge) return;

        // クリックされたら子生成        
        GameObject child = Instantiate(childPrefab, transform.position + spawnOffset, Quaternion.identity, transform);
        clickJudge = true;
        
    }
}
