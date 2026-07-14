using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;//处理所有物理效果
    
    // Start is called before the first frame update
    

    // Update is called once per frame
    void FixedUpdate()  //改成fixedUpdate，每秒50次，保证物理效果的稳定性
    {
        float horizontal = Input.GetAxis("Horizontal");//水平 监听左右 ad 键
        float vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontal,vertical)*speed; //velocity 是速度，Vector2 是一个二维向量，表示物体在 x 和 y 方向上的速度
        //直接改速度，但是保留碰撞，相比transform直接改位置（穿模） 会有更好的物理效果

    }
}
