using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParticle : MonoBehaviour
{
    public ParticleSystem LeftParticle;
    public ParticleSystem RightParticle;
    public Rigidbody Rigidbody;
    private float _intensity;

    private void Start()
    {
        LeftParticle.Play();
        RightParticle.Play();
    }

    private void Update()
    {
        _intensity = Rigidbody.velocity.sqrMagnitude / 10f;
        LeftParticle.maxParticles = (int)_intensity;
        RightParticle.maxParticles = (int)_intensity;
    }
}
