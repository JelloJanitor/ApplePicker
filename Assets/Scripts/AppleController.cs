using UnityEngine;

public class AppleController : MonoBehaviour
{
    // Y at which apples will be destroyed
    public static float bottomY = -20f;

    // Runs every frame
    void Update()
    {
        // Check if the apple has passed the minimum permissable y value
        if (transform.position.y < bottomY)
        {
            // Notify gamemanager an apple was missed
            GameManager.Instance.AppleMissed();
        }
    }

    //// Apple deletes itself
    //public void DeleteApple()
    //{
    //    Destroy(gameObject);
    //}
}
