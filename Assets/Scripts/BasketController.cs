using UnityEngine;
using UnityEngine.InputSystem;

public class BasketController : MonoBehaviour
{
    private void OnCollisionEnter(Collision coll)
    {
        // Call UpdateScore
        GameManager.Instance.ItemCaught(coll.gameObject);
    }
}
