using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject player;


    private void Update()
    {
        if (player == null)
        {
            return;
        }


        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float cameraZ = transform.position.z;

        transform.position = new Vector3(playerX, playerY, cameraZ);

    }
}
