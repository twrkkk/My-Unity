using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool _isCollected;
    private void OnTriggerEnter(Collider other)
    {
        if(!_isCollected && other.gameObject.tag == "PlayerCar")
        {
            _isCollected = true;
            GameStateHandler.Instance.CollectCheckPoint();
        }
    }
}
