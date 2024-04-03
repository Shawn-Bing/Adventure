using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDetection : MonoBehaviour
{
    //检测半径
    public float checkRadius;
    //检测修正值
    public Vector2 bottomOffset;
    //检测的中心点
    private Vector2 checkCenter;
    //LayerMask
    public LayerMask groundLayer;
    //判断是否碰撞
    public bool isGround;

    public void Check()
    {
        checkCenter = (Vector2)transform.position + bottomOffset;
        //判断与地面是否发生碰撞，需要设置此物理检测组件内的LayerMask为地面层
        isGround = Physics2D.OverlapCircle(checkCenter,checkRadius,groundLayer);
    }
    //DrawCheckArea
    private void OnDrawGizmosSelected() {
        //WireSphere是一个2D圆，Sphere是个3D圆
        Gizmos.DrawWireSphere(checkCenter,checkRadius);
    }
    // Update is called once per frame
    private void Update()
    {
        Check();
    }
}
