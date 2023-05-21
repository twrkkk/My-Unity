using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AIAgentConfig : ScriptableObject
{
    public float UpdateTargetFrequency = 1f;
    public float MaxDistance = 5f;
    public float DieForce = 10f;
    public float MaxSightDistance = 5f; 
}
