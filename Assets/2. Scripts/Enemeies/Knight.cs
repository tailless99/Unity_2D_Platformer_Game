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
    public float walkSpeed = 3.0f;
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    touchingDirections touchingDirections;
    Animator animator;
        
    private Vector2 walkDirectionVector = Vector2.right;
    private WalkableDirection walkDirection = WalkableDirection.Right;
    [SerializeField] private bool _hasTarget = false;
    [SerializeField] private DetectionZone attackZone;

    public bool HasTarget {
        get { return _hasTarget; }
        private set 
        {
            _hasTarget = value;
            animator.SetBool("HasTarget", value);
        }
    }

    public bool CanMove { get { return animator.GetBool("CanMove"); } }

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
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColiders.Count > 0;
    }

    void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall) FlipDirection();

        if(CanMove) rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        else rb.velocity = new Vector2(0 * walkDirectionVector.x, rb.velocity.y);
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
}
