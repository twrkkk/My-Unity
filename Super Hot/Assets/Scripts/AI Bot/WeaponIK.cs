using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HumanBone
{
    public HumanBodyBones bone;
}

public class WeaponIK : MonoBehaviour
{
    public AIAgent agent;
    public Transform targetTransform;
    public Transform aimTransform;
    public Transform[] bones;
    public Vector3 targetOffset;
    public int precision = 10;
    [Range(0, 1)]
    public float weight = 1f;

    public float angleLimit = 90f;
    public float distanceLimit = 1.5f;

    Vector3 GetTarhetPosition()
    {
        Vector3 targetDirection = (targetTransform.position + targetOffset) - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > angleLimit)
            blendOut += (targetAngle - angleLimit) / 50f;

        float targetDistance = targetDirection.magnitude;
        if (targetDistance < distanceLimit)
            blendOut += distanceLimit - targetDistance;

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return aimTransform.position + direction;
    }

    private void LateUpdate()
    {
        if (agent.stateMachine.currentState == AIStateId.Death) return;
        if (targetTransform == null || aimTransform == null) return;

        Vector3 targetPosition = GetTarhetPosition();
        for (int i = 0; i < precision; i++)
        {
            //for (int b = 0; b < bones.Length; b++)
            {
                //Transform bone = boneTransforms[b];
                AimTarget(transform, targetPosition, weight);
            }
        }
    }

    private void AimTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendRotation * bone.rotation;
    }

    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
    }
    
    public void SetAimTransform(Transform aim)
    {
        aimTransform = aim;
    }
}
