
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{

    public GameObject Ball;

    public override void OnNetworkSpawn()
    {

        Transform spawn = GameObject.FindGameObjectsWithTag("Respawn")[OwnerClientId].transform;
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
        if (IsLocalPlayer)
        {
            GameObject.FindGameObjectWithTag("ServerCamera").GetComponent<Camera>().enabled = false;
            GetComponent<PlayerController>().EnablePlayerCamera(true);
        }
        /* if (IsHost)
        {
            GameObject go = Instantiate(Ball, Vector3.zero, Quaternion.identity);
            go.GetComponent<NetworkObject>().Spawn();
            go.transform.position = GameObject.FindGameObjectWithTag("RespawnBall").transform.position;
        } */

    }
}
