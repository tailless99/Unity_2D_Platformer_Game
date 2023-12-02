using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkableDirection
{
    Right,
    Left
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(touchingDirections))]
public class Knight : MonoBehaviour
{
    public float MaxSpeed = 3.0f;
    public float walkAcceleration = 7.0f;
    public float walkStopRate = 0.6f;

    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    touchingDirections touchingDirections;
    Animator animator;
    DamageAble damageable;
        
    private Vector2 walkDirectionVector = Vector2.right;
    private WalkableDirection walkDirection = WalkableDirection.Right;
    [SerializeField] private bool _hasTarget = false;
    [SerializeField] private DetectionZone attackZone;

    public float AttackCooltime
    {
        get { return animator.GetFloat(AnimationStrings.AttackCoolDown); }
        set { animator.SetFloat(AnimationStrings.AttackCoolDown, Mathf.Max(value, 0)); }
    }
    public bool HasTarget {
        get { return _hasTarget; }
        private set 
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.HasTarget, value);
        }
    }

    public bool CanMove { get { return animator.GetBool(AnimationStrings.CanMove); } }

    public WalkableDirection WalkDirection
    {
        get { return walkDirection; }
        set
        {
            if (walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right) walkDirectionVector = Vector2.right;
                else if (value == WalkableDirection.Left) walkDirectionVector = Vector2.left;
            }
            walkDirection = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        touchingDirections = GetComponent<touchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<DamageAble>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColiders.Count > 0;
        if(AttackCooltime > 0)
        {
            AttackCooltime -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall) FlipDirection();

        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGrounded) rb.velocity = 
                    new Vector2 {
                        // Clamp (val, min, max); val의 값을 min과 max값의 사이의 범위로 강제하는 함수
                        x = Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime),-MaxSpeed, MaxSpeed),
                        y = rb.velocity.y
                    };
            else rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, 0,walkStopRate), rb.velocity.y);
        }
    }

    private void FlipDirection() { 
        if(WalkDirection == WalkableDirection.Right) WalkDirection = WalkableDirection.Left;        
        else if(WalkDirection == WalkableDirection.Left) WalkDirection = WalkableDirection.Right;
    }

    public void OnCliffDetectes()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }

    public void OnHit(Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}