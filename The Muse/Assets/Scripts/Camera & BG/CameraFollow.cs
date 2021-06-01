using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;

    private void FixedUpdate()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}
