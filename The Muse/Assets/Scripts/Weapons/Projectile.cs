using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 startPos;
    private Rigidbody2D RB;

    public float[] attackDetails = new float[2];

    public int direction;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveProjectile();

        if (Mathf.Abs(transform.position.x) > startPos.x + 25 || Mathf.Abs(transform.position.x) < startPos.x - 25)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        RB.velocity = new Vector2(direction * 25, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damageable"))
        {
            other.gameObject.SendMessage("Damage", attackDetails);
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
