using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    private Player playerController;
    private void Awake()
    {
        playerController = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            playerController.AddClosestBall(other.gameObject.GetComponent<Ball>());
            HighLight highLight = other.gameObject.GetComponent<HighLight>();
            highLight.SwapMaterial();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            playerController.RemoveClosestBall(other.gameObject.GetComponent<Ball>());
            HighLight highLight = other.gameObject.GetComponent<HighLight>();
            highLight.SwapMaterial();
        }
    }
}
