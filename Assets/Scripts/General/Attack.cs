using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //定义攻击参数
    public int attackDamage;
    public float attackRange;
    public float attackRate;

    //传递参数给受击对象
    private void OnTriggerStay2D(Collider2D other) {
        //？的作用是判断other对象是否有Properties组件
        //相当于if(!=NULL)
        other.GetComponent<Properties>()?.TakeDamage(this);
    }

    //直接改变受击对象生命值
    // private void OnTriggerStay2D(Collider2D other) {
    //     other.GetComponent<Properties>().currentHealth -= attackDamage;
    // }
}
