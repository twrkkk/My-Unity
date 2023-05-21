using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCar")
        {
            Debug.Log("Cross finish line");
            GameStateHandler.Instance.WinGame();
        }
    }
}
