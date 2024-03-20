using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputController inputControl;
    //PIC是自定义的输入系统名，iC是别名，此时可以在Player脚本里调用IC变量

    public Vector2 inputDirection;
    private Rigidbody2D rb;//私有实例化刚体组件
    public float speedScale;//公开速度

    //为了实现移动功能撰写的函数
    public void Movement()
    {
        //修改组件速度,x轴速度 = x轴方向 * 自己设置的速度变量
        //y轴等于原来速度就可以（实际上等于全局设置的重力）
        rb.velocity = new Vector2(inputDirection.x * speedScale, rb.velocity.y);
    }

    //Awake是C#最初执行的函数
    private void Awake() {
        inputControl = new PlayerInputController();
        
        //获取使用权
        rb = GetComponent<Rigidbody2D>();
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

    //以固定帧率帧调用该函数，与物理有关的放这
    //在这里每帧调用移动函数
    private void FixedUpdate() {
        Movement();
    }
}
