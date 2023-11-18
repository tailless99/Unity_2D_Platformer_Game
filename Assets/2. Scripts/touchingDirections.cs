using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchingDirections : MonoBehaviour
{
    private bool _isGrounded = true;
    private bool _isOnWalled = true;
    private bool _isOnCeiling = true;

    public bool IsGrounded
    {
        get { return _isGrounded; }
        set
        {
            _isGrounded = value;
            animator.SetBool("IsGrounded", _isGrounded);
        }
    }

    public bool IsOnWall
    {
        get { return _isOnWalled; }
        set
        {
            _isOnWalled = value;
            animator.SetBool("IsOnWall", _isOnWalled);
        }
    }


    public bool IsOnCeiling
    {
        get { return _isOnCeiling; }
        set
        {
            _isOnCeiling = value;
            animator.SetBool("IsOnCeiling", _isOnCeiling);
        }
    }

    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private float wallDistance = 0.2f;
    [SerializeField] private float ceilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    public ContactFilter2D castFilter;
    Animator animator;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0 ? true : false;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0 ? true : false;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0 ? true : false;
    }
}
