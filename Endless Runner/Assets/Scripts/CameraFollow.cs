using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offset;

    private void Start()
    {
        _offset = _target.position - transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.position.z - _offset.z); 
    }
}
