using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PawnMoveToTarget : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Target == null)
        {
            return;
        }

        if (Mathf.Abs(Vector3.Distance(transform.position, Target.position)) > 0.1f)
        {
            Debug.Log("Setting Destination to: " + transform.position);
            agent.SetDestination(Target.position);
        }
    }
}
