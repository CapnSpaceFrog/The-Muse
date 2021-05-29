using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 startPos;
    private Rigidbody2D RB;

    public int direction;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void Update()
    {
        MoveProjectile();

        if (Mathf.Abs(transform.position.x) > Mathf.Abs(startPos.x) + 20)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        RB.velocity = new Vector2(direction * 25, 0);
    }
}
