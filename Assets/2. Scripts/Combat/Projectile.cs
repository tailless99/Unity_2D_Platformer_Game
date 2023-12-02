using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    public float timeLimit = 0.0f;
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2 (3.0f, 0);
    public Vector2 knockback = Vector2.zero;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void FixedUpdate()
    {
        timeLimit += Time.deltaTime;
        if(timeLimit > 4) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageAble damageable = collision.GetComponent<DamageAble>();
        if(damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.GetHit(damage, deliveredKnockback);

            if (gotHit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
