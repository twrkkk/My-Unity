using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;
    [SerializeField, Range(0f, 1f)] float _parallaxStrength = 0.1f;
    private Vector3 _startPos;

    private void Start()
    {
        if (!_followingTarget)
            _followingTarget = Camera.main.transform;

        _startPos = _followingTarget.position;
    }

    private void Update()
    {
        Vector3 delta = _followingTarget.position - _startPos;
        transform.position += delta * _parallaxStrength;
        _startPos = _followingTarget.position;
    }
}
