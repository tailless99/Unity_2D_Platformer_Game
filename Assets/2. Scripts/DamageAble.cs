using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageAble : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10; // 최대 체력
    [SerializeField] private int _health = 10; // 현재 체력    
    [SerializeField] private bool _isAlive = true; // 생존여부
    Animator animator;
    public UnityEvent<Vector2> damageableHit;
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.LockVelocity); }
        //set { animator.SetBool(AnimationStrings.LockVelocity, value); }
    }

    public bool IsAlive
    {
        get { return _isAlive; }
        set { 
            _isAlive = value;
            animator.SetBool(AnimationStrings.IsAlive, value);
        }
    }

    public int MaxHealth {  
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int Health
    {
        get { return _health; }
        set { 
            _health = value; 
            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GetHit(int attackDamage, Vector2 knockBack) {
        if (IsAlive)
        {
            Health -= attackDamage;
            if (Health > 0)
            {
                animator.SetTrigger(AnimationStrings.Hit);
                damageableHit?.Invoke(knockBack);
            }
        }
    }

    public bool Heal(int HealthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, HealthRestore);

            Health += actualHeal;
            Debug.Log(Health);
            return true;
        }
        return false;
    }
}
