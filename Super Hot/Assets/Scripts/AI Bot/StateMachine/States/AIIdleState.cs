using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    public void Enter(AIAgent agent)
    {
    }

    public void Exit(AIAgent agent)
    {
    }

    public AIStateId GetId()
    {
        return AIStateId.Idle;
    }

    public void Update(AIAgent agent)
    {
        Vector3 playerDirection = (agent.PlayerTransform.position - agent.transform.position);
        if(playerDirection.magnitude > agent.config.MaxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if(dotProduct > 0f)
        {
            agent.stateMachine.ChangeState(AIStateId.ChasePlayer);
        }
    }
}
