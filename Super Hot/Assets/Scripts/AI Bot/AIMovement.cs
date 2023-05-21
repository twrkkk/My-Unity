using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class AIMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_agent.hasPath)
            _animator.SetFloat("Speed", _agent.velocity.normalized.magnitude);
        else
            _animator.SetFloat("Speed", 0);
    }
}
