using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class PawnMoveToTarget2 : MonoBehaviour
{
    [Header("ターゲット名のリスト（ランダム選択用）")]
    public string[] targetNames;

    [Header("移動速度設定")]
    public float minSpeed = 2.0f;
    public float maxSpeed = 4.0f;

    [Header("回転設定")]
    public float rotateSpeed = 10f;

    public Transform Target;

    private NavMeshAgent agent;
    private vThirdPersonInput input;
    private List<Transform> targetList = new List<Transform>();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // NavMeshAgentの自動回転をOFF

        // ★ 速度をランダムで設定
        agent.speed = Random.Range(minSpeed, maxSpeed);

        // 名前リストからターゲットを収集
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

        // 最初のターゲットをランダム選択
        PickRandomTarget();
    }

    private void Update()
    {
        if (Target == null) return;

        // vThirdPersonInput があれば速度を同期
        if (input != null && input.gameObject.activeSelf && input.isActiveAndEnabled)
        {
            input.Velocity = agent.velocity;
        }
        else
        {
            input = GetFirstActiveInputInImmediateChildren();
            if (input != null)
            {
                input.Velocity = agent.velocity;
            }
        }

        // 移動
        float distance = Vector3.Distance(transform.position, Target.position);
        if (distance > 0.1f)
        {
            agent.SetDestination(Target.position);
            RotateToMoveDirection();
        }
    }

    private void RotateToMoveDirection()
    {
        Vector3 velocity = agent.velocity;

        // ほぼ止まっている時は回転しない
        if (velocity.sqrMagnitude < 0.01f) return;

        // 水平回転のみ
        velocity.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private void PickRandomTarget()
    {
        if (targetList.Count > 0)
        {
            Target = targetList[Random.Range(0, targetList.Count)];
        }
        else
        {
            Target = null;
        }
    }

    public vThirdPersonInput GetFirstActiveInputInImmediateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            GameObject childGameObject = childTransform.gameObject;

            if (childGameObject.activeSelf)
            {
                vThirdPersonInput input = childGameObject.GetComponent<vThirdPersonInput>();
                if (input != null && input.isActiveAndEnabled)
                {
                    return input;
                }
            }
        }

        return null;
    }
}
