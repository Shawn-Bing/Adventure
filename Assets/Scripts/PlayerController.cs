using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputController inputControl;
    public Vector2 inputDirection;

    private Rigidbody2D rb;
    private PhysicsDetection physicsDetection;

    [Header("基本参数")]
    public float speedScale;
    public float JumpForce;

    public void Movement()
    {
        //Move
        rb.velocity = new Vector2(inputDirection.x * speedScale, rb.velocity.y);
        //Flip
        int playerDir = (int)transform.localScale.x;
        if (inputDirection.x < 0) playerDir = -1;
        if (inputDirection.x > 0) playerDir = 1;
        transform.localScale = new Vector3(playerDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //检测是否在地面，若是，允许跳跃
        if(physicsDetection.isGround)
        {
            rb.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //获取物理检测
        physicsDetection = GetComponent<PhysicsDetection>();
        inputControl = new PlayerInputController();
        inputControl.GamePlay.Jump.started += Jump;
    }
    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Movement();
    }
}
