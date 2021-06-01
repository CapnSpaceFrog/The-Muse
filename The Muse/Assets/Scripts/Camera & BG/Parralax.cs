using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    private float length, startpos;

    public Camera cam;

    public float parralaxEffect;

    public float moveEffect;

    private void Start()
    {
        startpos = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parralaxEffect);
        float distance = (cam.transform.position.x * parralaxEffect);

        transform.position = new Vector3(startpos + distance, cam.transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }

    
}
