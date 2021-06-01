using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;

    private void LateUpdate()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}
