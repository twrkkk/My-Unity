using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        foreach (var c in collision.contacts)
        {
            if (c.otherCollider.GetComponent<BowlComponent>() == null)
            {
                if (GameStateHandler.Instance.GameState != GameState.Win)
                    GameStateHandler.Instance.LoseGame();
            }
        }
    }
}
