using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public GameObject Ball;
    private void Start()
    {
        if (isServer)
        {
            GameObject newBall = Instantiate(Ball, GameObject.FindGameObjectWithTag("RespawnBall").transform.position, Quaternion.identity);
            NetworkServer.Spawn(newBall);
        }
    }
}
