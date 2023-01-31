using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCamera : MonoBehaviour
{

    [SerializeField]
    [Range(0.1f, 1f)]
    private float MouseSensibility;

    private Vector3 rotation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        Vector2 lookVec = ctx.ReadValue<Vector2>();
        rotation.x = Mathf.Clamp(rotation.x + -lookVec.y * MouseSensibility, -90f, 90f);
        rotation.y = (rotation.y + lookVec.x * MouseSensibility) % 360f;
        transform.parent.localEulerAngles = rotation;
    }
}
