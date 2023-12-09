using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float runSpeend = 8;
    [SerializeField] private float airWalkSpeend = 3;
    [SerializeField] private float airRunSpeend = 5;
    [SerializeField] private float jumpImpulse = 10;
    Rigidbody2D rb;
    Vector2 moveInput;
    Animator animator;
    touchingDirections touchingDirections;
    DamageAble damageAble;
    private bool _isRunning = false;
    private bool _isMoving = false;
    private bool _isFacingRight = true;

    public bool CanMove {  get { return animator.GetBool(AnimationStrings.CanMove); } }
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (isMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (isRuning) return runSpeend;
                        else return walkSpeed;
                    }
                    else
                    {
                        if (isRuning) return airRunSpeend;
                        else return airWalkSpeend;
                    }
                }
                else {
                    return 0;
                }
            }
            else return 0;
        }
    }

    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.IsMoving, _isMoving);
        }
    }

    public bool isRuning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
             animator.SetBool(AnimationStrings.IsRunning, _isRunning);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<touchingDirections>();
    }

    private void SetFacingDirection(Vector2 moveinPut)
    {
        if (moveinPut.x > 0 && !IsFacingRight) IsFacingRight = true;
        else if (moveinPut.x < 0 && IsFacingRight) IsFacingRight = false;
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.RangedAttack);
        }
    }
    public void OnMove(InputAction.CallbackContext content)
    {
        moveInput = content.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }    

    public void OnRun(InputAction.CallbackContext content)
    {
        // 버튼이 눌렸을 때를 감지
        if (content.started)    isRuning = true;
        // 버튼을 땠을 때를 감지
        else if (content.canceled)   isRuning = false;
    }

    public void OnJump(InputAction.CallbackContext content) { 
        if(content.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.Jump);
            rb.velocity = new Vector2 (rb.velocity.x, jumpImpulse);
        }
    }

    public void OnFire(InputAction.CallbackContext content)
    {
        if (content.started)
        {
            animator.SetTrigger(AnimationStrings.Attack);
        }
    }

    private void FixedUpdate()
    {
        if (!damageAble) {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }        
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnHit(Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
