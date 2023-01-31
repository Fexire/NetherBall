using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform CameraTransform;

    private void LateUpdate() {
        transform.LookAt(transform.position + CameraTransform.forward);
    }
}
