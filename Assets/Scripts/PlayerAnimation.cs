using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;

    private void SetAnimation()
    {
        //将人物X轴移动速度赋值给我们创建的名为velocityX的浮点数

        //由于Velocity值是通过方向（带正负值）和速度乘积获得的，因此当向左时为负
        //我们设置的动画切换条件也只有大于某数值时切换，为了减少判断次数，这里可以直接将最后的vX值取绝对值
        ani.SetFloat("velocityX",Math.Abs(rb.velocity.x));
    }

    //获取对应组件
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    //每帧检测调用，切换动画
    private void Update() {
        SetAnimation();
    }

}
