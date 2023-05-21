using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeapon : MonoBehaviour
{
    public WeaponIK weaponIK;
    public Transform currentTarget;
    public Transform shootingPoint;
    public bool canFire;

    #region Fields
    [Header("Firing")]

    [Tooltip("Is this weapon automatic? If yes, then holding down the firing button will continuously fire.")]
    [SerializeField]
    private bool automatic;

    [Tooltip("How fast the projectiles are.")]
    [SerializeField]
    private float projectileImpulse = 400.0f;

    [Tooltip("Amount of shots this weapon can shoot in a minute. It determines how fast the weapon shoots.")]
    [SerializeField]
    private int roundsPerMinutes = 200;

    [Tooltip("Mask of things recognized when firing.")]
    [SerializeField]
    private LayerMask mask;

    [Tooltip("Maximum distance at which this weapon can fire accurately. Shots beyond this distance will not use linetracing for accuracy.")]
    [SerializeField]
    private float maximumDistance = 500.0f;

    [Header("Resources")]

    [Tooltip("Casing Prefab.")]
    [SerializeField]
    private GameObject prefabCasing;

    [Tooltip("Projectile Prefab. This is the prefab spawned when the weapon shoots.")]
    [SerializeField]
    private GameObject prefabProjectile;

    [Tooltip("The AnimatorController a player character needs to use while wielding this weapon.")]
    [SerializeField]
    public RuntimeAnimatorController controller;

    [Tooltip("Weapon Body Texture.")]
    [SerializeField]
    private Sprite spriteBody;

    [Header("Audio Clips Holster")]

    [Tooltip("Holster Audio Clip.")]
    [SerializeField]
    private AudioClip audioClipHolster;

    [Tooltip("Unholster Audio Clip.")]
    [SerializeField]
    private AudioClip audioClipUnholster;

    [Header("Audio Clips Reloads")]

    [Tooltip("Reload Audio Clip.")]
    [SerializeField]
    private AudioClip audioClipReload;

    [Tooltip("Reload Empty Audio Clip.")]
    [SerializeField]
    private AudioClip audioClipReloadEmpty;

    [Header("Audio Clips Other")]

    [Tooltip("AudioClip played when this weapon is fired without any ammunition.")]
    [SerializeField]
    private AudioClip audioClipFireEmpty;
    #endregion

    private void Start()
    {
        //StartFire();
    }

    public void StartFire()
    {
        canFire = true;
        StartCoroutine(Fire());
    }

    public void StopFire()
    {
        canFire = false;
        StopCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while(canFire)
        {
            //Determine the rotation that we want to shoot our projectile in.
            //Quaternion rotation = Quaternion.LookRotation(shootingPoint.position, weaponIK.agent.PlayerTransform.position);

            //Spawn projectile from the projectile spawn point.
            GameObject projectile = Instantiate(prefabProjectile, shootingPoint.position, Quaternion.Euler(shootingPoint.forward));
            //Add velocity to the projectile.
            Debug.Log("shoot");
            projectile.GetComponent<Rigidbody>().velocity = shootingPoint.forward * projectileImpulse;
            yield return new WaitForSeconds(60f / roundsPerMinutes);
        }
        
    }

    public void ThrowWeapon(Vector3 direction) //
    {
        transform.parent = null;
        GetComponentInChildren<BoxCollider>().enabled = true;
        Rigidbody rb;
        gameObject.TryGetComponent<Rigidbody>(out rb);
        if (!rb)
            gameObject.AddComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        //GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
    }

    public void SetTarget(Transform target)
    {
        weaponIK.SetTargetTransform(target);
        currentTarget = target;
    }
}
