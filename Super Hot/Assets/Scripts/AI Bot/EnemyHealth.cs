using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ragdoll))]    
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth; // for example 100f
    private float _currentHealth;
    private AIAgent _agent;
    private Rigidbody[] _rbs;
    private void Start()
    {
        _currentHealth = _maxHealth;
        _agent = GetComponent<AIAgent>();
        _rbs = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in _rbs)
        {
            PartOfEnemysBody enemyPart = rb.GetComponent<PartOfEnemysBody>();
            enemyPart._healthComponent = this;
        }
    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
            Die(direction);
    }

    private void Die(Vector3 direction)
    {
        AIDeathState deathState = _agent.stateMachine.GetState(AIStateId
            .Death) as AIDeathState;
        deathState.direction = direction;
        _agent.stateMachine.ChangeState(AIStateId.Death);
    }
}
