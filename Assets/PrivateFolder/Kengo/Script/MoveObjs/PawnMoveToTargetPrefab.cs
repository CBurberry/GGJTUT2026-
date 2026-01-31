using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class PawnMoveToTargetPrefab : MonoBehaviour
{
    [Header("ターゲット名のリスト（ランダム選択用）")]
    public string[] targetNames;

    [Header("ランダム切り替え間隔")]
    public float switchInterval = 5f;

    [Header("移動速度設定")]
    public float minSpeed = 2.0f;
    public float maxSpeed = 4.0f;

    [Header("回転設定")]
    public float rotateSpeed = 10f;

    private List<Transform> targetList = new List<Transform>();
    private Transform Target;
    private NavMeshAgent agent;
    private float switchTimer = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // ★ NavMeshAgentの自動回転は使わない
        agent.updateRotation = false;

        // 速度を最小〜最大の範囲でランダム決定
        agent.speed = Random.Range(minSpeed, maxSpeed);

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

        PickRandomTarget();
    }

    private void Update()
    {
        if (Target == null) return;

        // 移動
        float distance = Vector3.Distance(transform.position, Target.position);
        if (distance > 0.1f)
        {
            agent.SetDestination(Target.position);
        }

        // ★ 移動方向にY軸回転
        RotateToMoveDirection();

        // ターゲット切り替え
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

    private void RotateToMoveDirection()
    {
        Vector3 velocity = agent.velocity;

        // ほぼ止まっている時は回転しない
        if (velocity.sqrMagnitude < 0.01f) return;

        // Y成分を消して水平回転のみ
        velocity.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

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
