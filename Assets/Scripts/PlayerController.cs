using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputController inputControl;
    //PIC是自定义的输入系统名，iC是别名，此时可以在Player脚本里调用IS变量

    public Vector2 inputDirection; 
    
    //Awake是C#最初执行的函数
    private void Awake() {
        inputControl = new PlayerInputController();
    }
    private void OnEnable() {
        inputControl.Enable();
    }
    private void OnDisable() {
        inputControl.Disable();
    }

    void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        //创建一个测试变量叫inputDirection
        //inputControl里的GamePlay模板中的Move行为中的ReadValue方法        
    }
}
