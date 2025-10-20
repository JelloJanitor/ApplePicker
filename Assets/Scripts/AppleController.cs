using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    public static float bottomY = -20f;

    private void OnEnable()
    {
        //GameManager.Instance.OnAppleMiss += DeleteApple;
    }

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            // Notify GameManager AFTER destroy call, but only if this object is still valid
            // Use a guard to prevent further execution if already destroyed
            // (Destroy happens end of frame, so this is safe)
            GameManager.Instance.AppleMissed();
        }
    }

    public void DeleteApple()
    {
        Destroy(gameObject);
    }
}
