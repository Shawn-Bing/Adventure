using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    #region 创建类实例

    public PlayerInputController inputControl;
    public Vector2 inputDirection;

    private Rigidbody2D rb;
    private PhysicsDetection physicsDetection;

    #endregion

    [Header("基本参数")]
    public float speedScale;
    public float JumpForce;
    private float currentSpeed;
    public bool isCrouch;//存放下蹲状态

    public void Movement()
    {
        //Move
        rb.velocity = new Vector2(inputDirection.x * speedScale, rb.velocity.y);
        //Flip
        int playerDir = (int)transform.localScale.x;
        if (inputDirection.x < 0) playerDir = -1;
        if (inputDirection.x > 0) playerDir = 1;
        transform.localScale = new Vector3(playerDir, 1, 1);
        //Crouch
        isCrouch = physicsDetection.isGround && inputDirection.y < -0.1f;//在地面，且垂直速度为负（按了下方向键）
        
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
        //绑定Jump函数到Jump按键按下
        inputControl.GamePlay.Jump.started += Jump;


        #region 潜行键
        //暂存Speed
        currentSpeed = speedScale;
        //修改速度并绑定到潜行键
        inputControl.GamePlay.Sneak.performed += ctx =>
        {
            if(physicsDetection.isGround)
            speedScale = currentSpeed/2.5f;
        };
        //松开键位后速度回正
        inputControl.GamePlay.Sneak.canceled += ctx =>
        {
            if(physicsDetection.isGround)
            speedScale = currentSpeed;
        };
        #endregion
        
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
