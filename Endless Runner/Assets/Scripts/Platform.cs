using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _levelParent;

    private void Awake()
    {
        _parent = GameObject.FindGameObjectWithTag("pool").transform;
        _levelParent = GameObject.FindGameObjectWithTag("level").transform;
    }

    private void OnEnable()
    {
        transform.parent = _levelParent;
    }

    public void ToPool()
    {
        transform.parent = _parent;
        gameObject.SetActive(false);
    }
}
