using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfimaGames.LowPolyShooterPack;

public class BulletsItem : MonoBehaviour
{
    [SerializeField] private int _bulletsCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Character>()?.EquippedWeapon().FillAmmunition(_bulletsCount);
            Destroy(gameObject);
        }
    }
}

