using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Ball : NetworkBehaviour
{

    [SerializeField]
    private float ballSpeed;
    private Rigidbody RigidBody;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    public void Catch()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        RigidBody.AddForce(Vector3.up * ballSpeed, ForceMode.Impulse);
    }

    public void Throw(Transform newTransform, Vector3 velocity)
    {
        this.gameObject.SetActive(true);
        transform.position = newTransform.position + newTransform.forward * 2;
        transform.rotation = newTransform.rotation;
        RigidBody.velocity = Vector3.zero;
        RigidBody.AddForce(newTransform.forward * ballSpeed + velocity / 10f, ForceMode.Impulse);
    }

}
