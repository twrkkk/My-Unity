using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{
    public Vector3 direction;

    public void Enter(AIAgent agent)
    {
        agent.NavMeshAgent.enabled = false;
        agent.Ragdoll.ActivateRagdoll();
        direction.y = 1;
        agent.Ragdoll.ApplyForce(direction * agent.config.DieForce);
        agent.Weapon.ThrowWeapon(agent.transform.position - agent.PlayerTransform.position);
        LevelHandler.instance.CurrentEnemyCount--;
    }

    public void Exit(AIAgent agent)
    {
    }

    public AIStateId GetId()
    {
        return AIStateId.Death;
    }

    public void Update(AIAgent agent)
    {
    }
}
