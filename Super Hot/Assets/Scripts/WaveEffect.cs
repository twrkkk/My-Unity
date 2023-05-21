using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, 1.2f + Mathf.PingPong(Time.time / 10f, 1f), transform.position.z);
        transform.position = pos;    
    }
}
