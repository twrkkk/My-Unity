using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChasePlayerState : AIState
{
    private float _timer;
    public void Enter(AIAgent agent)
    {
        agent.NavMeshAgent.stoppingDistance = agent.config.MaxDistance;
        
    }

    public void Exit(AIAgent agent)
    {
    }

    public AIStateId GetId()
    {
        return AIStateId.ChasePlayer;
    }

    public void Update(AIAgent agent)
    {
        UpdateAIPosition(agent);
    }

    private void UpdateAIPosition(AIAgent agent)
    {
        if (!agent.enabled) return;

        _timer -= Time.deltaTime;
        if(!agent.NavMeshAgent.hasPath)
        {
            agent.NavMeshAgent.destination = agent.PlayerTransform.position;
        }

        if (_timer < 0f)
        {
            Vector3 direction = (agent.PlayerTransform.position - agent.NavMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.MaxDistance * agent.config.MaxDistance)
            {
                agent.NavMeshAgent.destination = agent.PlayerTransform.position;
            }
            else
            {
                agent.stateMachine.ChangeState(AIStateId.AttackPlayer);
            }
            _timer = agent.config.UpdateTargetFrequency;
        }
    }
}
