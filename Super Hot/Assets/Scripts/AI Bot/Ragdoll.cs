using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    private Rigidbody[] _rbs;
    private Animator _animator;

    private void Awake()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        SetUpRagdoll();
    }

    public void ActivateRagdoll()
    {
        SetUpRagdoll(false);
    }

    public void ApplyForce(Vector3 force)
    {
        //var rigidBody = _animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        //rigidBody?.AddForce(force, ForceMode.VelocityChange);
        foreach (var rigidBody in _rbs)
        {
            rigidBody.AddForce(force, ForceMode.VelocityChange);
        }
    }

    private void SetUpRagdoll(bool isActive = true)
    {
        foreach (var rb in _rbs)
        {
            rb.isKinematic = isActive;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            if (isEnemy)
                rb.gameObject.AddComponent<PartOfEnemysBody>();
            else if (isActive)
            {
                PartOfHostage part = rb.gameObject.AddComponent<PartOfHostage>();
                part.Ragdoll = this;
            }
        }
        _animator.enabled = isActive;
    }
}
