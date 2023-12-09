using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2;
    public float waypointReachedDistance = 0.3f;
    public List<Transform> wayPoint = new List<Transform>();
    public Collider2D deathCollider;
    public float AttackCoolDown
    {
        get { return animator.GetFloat(AnimationStrings.AttackCoolDown); }
        set
        {
            animator.SetFloat(AnimationStrings.AttackCoolDown, Mathf.Max(value, 0));
        }
    }
    private bool _hasTarget = false;
    public bool HasTarget {  
        get { return _hasTarget; }
        set { 
            _hasTarget = value;
            animator.SetBool(AnimationStrings.HasTarget, value);
        }
    }

    private bool _canMove = false;
    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.CanMove); }
    }

    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    DamageAble damageAble;
    Transform nextWayPoint;
    public DetectionZone hitBoxZone;
    private int wayPointIndex = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageAble = GetComponent<DamageAble>();
    }

    private void Start()
    {
        nextWayPoint = wayPoint[wayPointIndex];
        this.transform.position = wayPoint[wayPointIndex].position;
    }

    private void Update()
    {
        HasTarget = hitBoxZone.detectedColiders.Count > 0;

        if(AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (damageAble.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    
    private void Flight()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - this.transform.position).normalized;

        float distance = Vector2.Distance(nextWayPoint.position, this.transform.position);

        rb.velocity = directionToWayPoint * flightSpeed;
        if (distance <= waypointReachedDistance)
        {
            wayPointIndex++;

            // 웨이포인트가 끝까지 도달했으면, 초기 포인트로 다시 돌린다.
            if (wayPointIndex >= wayPoint.Count) wayPointIndex = 0;

            nextWayPoint = wayPoint[wayPointIndex];
        }
    }

    public void OnDeath()
    {
        rb.gravityScale = 2.0f;
        deathCollider.enabled = true;
    }
}
