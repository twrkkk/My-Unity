using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DebugAIMovement : MonoBehaviour
{
    public bool ShowVelocity;
    public bool ShowDesiredVelocity;
    public bool ShowPath;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();  
    }
    private void OnDrawGizmos()
    {
        if(ShowVelocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _agent.velocity);
        }

        if(ShowDesiredVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _agent.desiredVelocity);
        }

        if(ShowPath)
        {
            Gizmos.color = Color.black;
            var agentPath = _agent.path;
            Vector3 prevCorner = transform.position;
            foreach (var corner in agentPath.corners)
            {
                Gizmos.DrawLine(prevCorner, corner);
                Gizmos.DrawSphere(corner, 0.2f);
                prevCorner = corner;
            }
        }
    }
}
