using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    public static float bottomY = -20f;
    
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);

            // Get a reference to the ApplePicker component of Main Camera
            ApplePickerController applePicker = Camera.main.GetComponent<ApplePickerController>();
            // Call the public AppleMissed() method of apScript
            applePicker.AppleMissed();
        }
    }
}
