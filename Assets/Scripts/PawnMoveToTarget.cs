using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PawnMoveToTarget : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent agent;

    [SerializeField]
    private vThirdPersonInput input;

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

        if (input != null) 
        {
            input.Velocity = agent.velocity;
        }

        if (Mathf.Abs(Vector3.Distance(transform.position, Target.position)) > 0.1f)
        {
            agent.SetDestination(Target.position);
        }
    }
}
