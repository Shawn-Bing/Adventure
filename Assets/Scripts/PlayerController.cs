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

    [Header("基本参数")]
    public float speedScale;
    
    //这个变量是为了施加一个向上的力让人物跳跃起来
    public float JumpForce;

    //为了实现移动功能撰写的函数
    public void Movement()
    {
        //移动
        rb.velocity = new Vector2(inputDirection.x * speedScale, rb.velocity.y);

        //翻转
        int playerDir = (int)transform.localScale.x;
        if(inputDirection.x < 0) playerDir = -1;
        if(inputDirection.x > 0) playerDir = 1;
        transform.localScale = new Vector3(playerDir,1,1);
    }

    //为了实现跳跃的函数，由VS自动生成
    private void Jump(InputAction.CallbackContext context)
    {
        //Debug用的一句，类似print
        //Debug.Log("You Pressed jump button");

        //自动生成的，这句没啥用，可以删
        //throw new NotImplementedException();

        //给人物刚体组件添加向上的力,transform.up代表这个组件正上方,力的模式是瞬时力
        rb.AddForce(transform.up * JumpForce,ForceMode2D.Impulse);
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
        //Option + 上下箭头键可以更改代码次序而不用复制粘贴
        inputControl = new PlayerInputController();

        //将Jump这个函数注册到跳跃键被按下的那一刻
        inputControl.GamePlay.Jump.started += Jump;
    }
    private void OnEnable() {
        inputControl.Enable();
    }
    private void OnDisable() {
        inputControl.Disable();
    }
    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();      
    }
    private void FixedUpdate() {
        Movement();
    }
}
