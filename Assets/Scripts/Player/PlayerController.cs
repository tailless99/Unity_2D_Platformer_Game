using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    private float walkSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    // A 키가 눌렸을 때 호출
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        moveInput.x -= 1;
    //    }
    //    // D 키가 눌렸을 때 호출
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        moveInput.x += 1;
    //    }
    //    else moveInput.x = 0;
    //}
    public void OnMove(InputAction.CallbackContext content) { 
        moveInput = content.ReadValue<Vector2>();
    }

    
}
