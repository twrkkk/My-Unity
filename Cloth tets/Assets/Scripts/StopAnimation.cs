using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimation : MonoBehaviour
{
    [SerializeField] Animator _animator;
    public void ChangeAnimState()
    {
        _animator.enabled = !_animator.enabled;
    }
}
