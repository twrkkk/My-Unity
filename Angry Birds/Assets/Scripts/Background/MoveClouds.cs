using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClouds : MonoBehaviour
{
    [SerializeField] private GameObject _clouds;
    [SerializeField] private float _speed;
    void Update()
    {
        _clouds.transform.position += new Vector3(-Time.deltaTime, 0, 0) * _speed;
    }
}
