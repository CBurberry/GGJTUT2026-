using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PawnMoveToTargetPrefab : MonoBehaviour
{
    [Header("ターゲットの名前で自動検索")]
    public string targetName = "Target"; // シーン上のオブジェクト名を指定
    private Transform Target;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Target が未設定の場合は名前で検索
        if (Target == null && !string.IsNullOrEmpty(targetName))
        {
            GameObject targetObj = GameObject.Find(targetName);
            if (targetObj != null)
            {
                Target = targetObj.transform;
            }
            else
            {
                Debug.LogWarning($"ターゲット名 {targetName} が見つかりませんでした。");
            }
        }
    }

    private void Update()
    {
        if (Target == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, Target.position);
        if (distance > 0.1f)
        {
            Debug.Log("Setting Destination to: " + Target.position);
            agent.SetDestination(Target.position);
        }
    }
}

