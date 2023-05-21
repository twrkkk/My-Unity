using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DeleteCoin());
    }

    IEnumerator DeleteCoin()
    {
        yield return new WaitForSeconds(25 * (5 / PlayerController.Instance.GetSpeed()));
        if (gameObject.activeSelf)
            ObjectPool.Instance.Coins.AddToPool(gameObject.GetComponent<Coin>());
    }
}
