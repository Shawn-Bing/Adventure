using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;
    private PhysicsDetection phsicDtc;
    private PlayerController plrCtl;

    private void SetAnimation()
    {
        ani.SetFloat("velocityX",Math.Abs(rb.velocity.x));
        ani.SetFloat("velocityY",rb.velocity.y);//不用去掉负值
        ani.SetBool("isGround",phsicDtc.isGround);//获取物理检测中地面监测
        ani.SetBool("isCrouch",plrCtl.isCrouch);
    }

    //获取对应组件
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        phsicDtc = GetComponent<PhysicsDetection>();
        plrCtl = GetComponent<PlayerController>();
    }
    //每帧检测调用，切换动画
    private void Update() {
        SetAnimation();
    }

}
