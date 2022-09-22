using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Rotate towards the camera
        transform.LookAt(Camera.main.transform);
    }
}
