using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    //基本属性参数
    [Header("基础属性")]
    public float maxHealth;//这个值在引擎中设置
    public float currentHealth;

    [Header("无敌时间")]
    public float invulnerableDuration;//无敌时间，这个值在引擎中设置
    private float invulnerableCounter;//计时器，将Duration量化为引擎时间
    public bool isInvulnerable;//检测是否无敌
    
    //开启无敌
    private void Invulnerable()
    {
        isInvulnerable = true;
        invulnerableCounter = invulnerableDuration;
    }
    //当触发了无敌时间时,在Update中使用这个计时器
    private void InvulnerableTimer()
    {
        if(isInvulnerable)
        {
            invulnerableCounter -= Time.deltaTime;//让机器读秒运行
            
            //当计时器归零，退出无敌状态
            //为什么不是==0？因为以帧为单位递减很快就会减少到小于0
            if(invulnerableCounter <= 0)
            isInvulnerable = false;
        }
    }

    //受击者接收来自攻击者的伤害
    public void TakeDamage(Attack attacker)
    {
        if(isInvulnerable)return;//若无敌，提前结束伤害计算
        
        //若此次攻击扣血后生命值为正
        if(currentHealth - attacker.attackDamage > 0)
        {
            currentHealth -= attacker.attackDamage;//若收到伤害，开启无敌
            Invulnerable();
        }else{
            //血量小于攻击伤害值，使其为0
            currentHealth = 0;
        }
        
    }
    private void Awake() {
        currentHealth = maxHealth;//初始化生命值
    }
    private void Update() {
        InvulnerableTimer();
    }

    //测试碰撞
    // private void OnTriggerStay2D(Collider2D other) {
    //     Debug.Log(other.name);
    // }
}
