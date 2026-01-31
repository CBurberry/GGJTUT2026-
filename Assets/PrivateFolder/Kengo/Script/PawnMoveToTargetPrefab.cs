using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class PawnMoveToTargetPrefab : MonoBehaviour
{
    [Header("ターゲット名のリスト（ランダム選択用）")]
    public string[] targetNames; // シーン上のオブジェクト名を入力

    [Header("ランダム切り替え間隔")]
    public float switchInterval = 5f; // 秒

    private List<Transform> targetList = new List<Transform>();
    private Transform Target;
    private NavMeshAgent agent;
    private float switchTimer = 0f;
    private float currentSpeed = 0f;
    private float targetSpeed = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // 名前リストからTransformを探して targetList に追加
        foreach (string name in targetNames)
        {
            GameObject obj = GameObject.Find(name);
            if (obj != null)
            {
                targetList.Add(obj.transform);
            }
            else
            {
                Debug.LogWarning($"ターゲット名 {name} が見つかりませんでした。");
            }
        }

        // 速度の初期化: 現在の速度を平均として 1/2〜2倍をランダム
        float baseSpeed = agent.speed;
        targetSpeed = Random.Range(baseSpeed * 0.5f, baseSpeed * 2f);
        currentSpeed = agent.speed;

        // 最初のターゲットをランダム選択
        PickRandomTarget();
    }

    private void Update()
    {
        if (Target == null) return;

        // NavMeshAgentで追従
        float distance = Vector3.Distance(transform.position, Target.position);
        if (distance > 0.1f)
        {
            agent.SetDestination(Target.position);
        }

        // ターゲット切り替えタイマー
        if (targetList.Count > 1)
        {
            switchTimer += Time.deltaTime;
            if (switchTimer >= switchInterval)
            {
                switchTimer = 0f;
                PickRandomTarget();
            }
        }
    }

    // ランダムでターゲットを選択
    private void PickRandomTarget()
    {
        if (targetList.Count > 0)
        {
            int index = Random.Range(0, targetList.Count);
            Target = targetList[index];
        }
        else
        {
            Target = null;
        }
    }
}
