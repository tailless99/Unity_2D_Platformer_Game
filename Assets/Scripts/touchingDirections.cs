using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchingDirections : MonoBehaviour
{
    private bool _isGrounded = true;
    public bool IsGrounded { 
        get { return _isGrounded; }
        set { 
            _isGrounded = value;
            animator.SetBool("IsGrounded", _isGrounded);
        }
    }

    [SerializeField] private float groundDistance = 0.05f;
    CapsuleCollider2D touchingCol;
    public ContactFilter2D castFilter;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0 ? true : false;
    }
}
