using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillClothColliders : MonoBehaviour
{
    [SerializeField] private Cloth _cloth;
    void Start()
    {
        CapsuleCollider[] colliders = FindObjectsOfType<CapsuleCollider>();
        _cloth.capsuleColliders = colliders;
        //foreach (var collider in colliders)
        //{
        //    Destroy(collider.GetComponent<Rigidbody>());
        //}
    }
}
