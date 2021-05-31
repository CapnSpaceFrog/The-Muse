using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditProjectile : MonoBehaviour
{
    private Vector2 startPos;
    private Rigidbody2D RB;
    [SerializeField]
    private enemyData stateData;

    [HideInInspector]
    public float[] attackDetails = new float[3];

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
        RB.velocity = new Vector2(direction * stateData.ProjectileSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            attackDetails[0] = stateData.ProjectileDamage;
            attackDetails[1] = transform.position.x;
            attackDetails[2] = stateData.KnockbackForce;
            other.gameObject.SendMessage("TakeDamage", attackDetails);
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
