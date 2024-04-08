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
    private CapsuleCollider2D cpsCld2D;

    #endregion

    [Header("基本参数")]
    public float speedScale;
    public float JumpForce;
    private float currentSpeed;
    public bool isCrouch;//存放下蹲状态
    public bool isHurt;//存放受伤状态
    public float HurtForce;//设置击退的力，Michele：8

    //保存原始Size&Offset的变量
    private Vector2 originCapsuleCollider2DSize;
    private Vector2 originCapsuleCollider2DOffset;

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
        if (isCrouch)
        {
            //修改碰撞体Size&Offset
                //此数据是自己尝试来的
            cpsCld2D.offset = new Vector2(originCapsuleCollider2DOffset.x, 0.7f);//Michele：0.85
            cpsCld2D.size = new Vector2(originCapsuleCollider2DSize.x, 1.4f);//Michele：1.7
        }
        else
        {
            //还原Size&Offset
            cpsCld2D.offset = originCapsuleCollider2DOffset;
            cpsCld2D.size = originCapsuleCollider2DSize;
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //检测是否在地面，若是，允许跳跃
        if (physicsDetection.isGround)
        {
            rb.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
        }
    }


    //实现受击后退
    public void GetHurt(Transform attacker)
    {
        isHurt = true;//使受伤状态为真
        //冻结玩家速度，消除惯性
        rb.velocity = Vector2.zero;
        //使玩家后退一段距离
            //首先计算后退的方向并归一化,等于人物坐标x轴值减去攻击者坐标x轴值（只考虑水平方向击退）
        Vector2 hurtDir = new Vector2((transform.position.x - attacker.position.x),0).normalized;
        //添加一个（击退方向*击退力）
        rb.AddForce(HurtForce * hurtDir,ForceMode2D.Impulse);
    }
    private void Awake()
    {
        #region 获取组件
        rb = GetComponent<Rigidbody2D>();
        physicsDetection = GetComponent<PhysicsDetection>();
        cpsCld2D = GetComponent<CapsuleCollider2D>();

        inputControl = new PlayerInputController();
        #endregion

        //绑定Jump函数到Jump按键按下
        inputControl.GamePlay.Jump.started += Jump;


        #region 潜行键
        //暂存Speed
        currentSpeed = speedScale;
        //修改速度并绑定到潜行键
        inputControl.GamePlay.Sneak.performed += ctx =>
        {
            if (physicsDetection.isGround)
                speedScale = currentSpeed / 2.5f;
        };
        //松开键位后速度回正
        inputControl.GamePlay.Sneak.canceled += ctx =>
        {
            if (physicsDetection.isGround)
                speedScale = currentSpeed;
        };
        #endregion

        //保存原始碰撞体数据，要在一开始就获取
        originCapsuleCollider2DSize = cpsCld2D.size;
        originCapsuleCollider2DOffset = cpsCld2D.offset;
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
        //若受伤，冻结玩家移动
        if(!isHurt)
        {
            Movement();
        }
    }
}
