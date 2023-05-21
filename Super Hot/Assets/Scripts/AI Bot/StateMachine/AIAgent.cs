using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Ragdoll))]
public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateId initialState;
    public NavMeshAgent NavMeshAgent;
    public AIAgentConfig config;
    public Ragdoll Ragdoll;
    public Transform PlayerTransform;
    public AIWeapon Weapon;

    public bool DrawStateSphere;

    private void Start()
    {
        if (!PlayerTransform)
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Ragdoll = GetComponent<Ragdoll>();
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIAttackPlayerState());
        stateMachine.ChangeState(initialState);
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnDrawGizmos()
    {
        if (!DrawStateSphere) return;

        switch (stateMachine.currentState)
        {
            case AIStateId.Idle:
                Gizmos.color = Color.green;
                break;
            case AIStateId.ChasePlayer:
                Gizmos.color = Color.yellow;
                break;
            case AIStateId.AttackPlayer:
                Gizmos.color = Color.red;
                break;
            default:
                break;
        }

        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
