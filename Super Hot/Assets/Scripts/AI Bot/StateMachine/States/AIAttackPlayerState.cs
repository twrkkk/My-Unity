using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackPlayerState : AIState
{
    float _timer = 0f;
    public void Enter(AIAgent agent)
    {
        agent.Weapon.SetTarget(agent.PlayerTransform);
        agent.NavMeshAgent.stoppingDistance = 5f;
        agent.Weapon.StartFire();
    }

    public void Exit(AIAgent agent)
    {
        agent.NavMeshAgent.stoppingDistance = 0f;
        agent.Weapon.StopFire();
    }

    public AIStateId GetId()
    {
        return AIStateId.AttackPlayer;
    }

    public void Update(AIAgent agent)
    {
        UpdateAIPosition(agent);
    }

    private void UpdateAIPosition(AIAgent agent)
    {
        if (!agent.enabled) return;

        _timer -= Time.deltaTime;
        if (!agent.NavMeshAgent.hasPath)
        {
            agent.NavMeshAgent.destination = agent.PlayerTransform.position;
        }

        if (_timer < 0f)
        {
            Vector3 direction = (agent.PlayerTransform.position - agent.NavMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.MaxDistance * agent.config.MaxDistance)
            {
                //agent.NavMeshAgent.destination = agent.PlayerTransform.position;
                agent.stateMachine.ChangeState(AIStateId.Idle);
            }
            
            _timer = agent.config.UpdateTargetFrequency;
        }
    }
}
