using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputController inputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rb;
    public float speedScale;

    //为了实现移动功能撰写的函数
    public void Movement()
    {
        //移动
        rb.velocity = new Vector2(inputDirection.x * speedScale, rb.velocity.y);

        //翻转
        //直接修改transform组件里的x轴方向即可

        //定义一个玩家朝向变量，让向左移动时这个变量为负，向右时为正
        int playerDir = (int)transform.localScale.x;
        if(inputDirection.x < 0) playerDir = -1;
        if(inputDirection.x > 0) playerDir = 1;

        //if(inputDirection.x == 0) playerDir = 0;
            //如果加一个这个判断，会让玩家在x轴丢失碰撞体积

        //else playerDir = 1;
            //如果改为这种判断，因为放开移动键位之后inputDirection默认为0，所以只要松开移动按键就会回到朝向右边
        transform.localScale = new Vector3(playerDir,1,1);
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
