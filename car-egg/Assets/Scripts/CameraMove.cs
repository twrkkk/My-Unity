using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 StartRotation;
    public Transform Target;
    public Rigidbody Rigidbody;
    [Range(0.1f, 30f)]
    public float _intensity = 1f;
    public bool Follow;
    private List<Vector3> VelocitiesList = new List<Vector3>();

    private void Start()
    {
        transform.rotation = Quaternion.Euler(StartRotation.x, StartRotation.y, StartRotation.z);
        for (int i = 0; i < 5; i++)
            VelocitiesList.Add(Vector3.zero);
    }

    private void Update()
    {
        transform.position = Target.position;
        float vel = Rigidbody.velocity.sqrMagnitude;
        Follow = (vel > 0.2f)? true : false;  
        if (!Follow) return;
        Vector3 lookAt = Vector3.zero;

        VelocitiesList.Add(Rigidbody.velocity);
        for (int i = 0; i < 5; i++)
        {
            lookAt += VelocitiesList[i];//Rigidbody.velocity;
        }
        lookAt -= VelocitiesList[0];
        VelocitiesList.RemoveAt(0);

        

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAt), Time.deltaTime * _intensity);
    }
}
