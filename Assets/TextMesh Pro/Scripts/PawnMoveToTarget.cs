using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PawnMoveToTarget : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent agent;
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

        if (Mathf.Abs(Vector3.Distance(transform.position, Target.position)) > 0.1f)
        {
            agent.SetDestination(Target.position);
        }
    }

    public vThirdPersonInput GetFirstActiveInputInImmediateChildren()
    {
        // Iterate through all immediate children of the parent Transform
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            GameObject childGameObject = childTransform.gameObject;

            // Check if the child GameObject is active in the hierarchy
            if (childGameObject.activeSelf)
            {
                // Try to get the component from the active child
                vThirdPersonInput input = childGameObject.GetComponent<vThirdPersonInput>();

                // If the component is found (and active/enabled, depending on the component type's properties)
                if (input != null && input.isActiveAndEnabled)
                {
                    return input; // Return the first one found
                }
            }
        }

        return null; // Return null if no active component is found in children
    }
}
