using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public int facingDirection = 1; //记录面朝方向 1表示向右，-1表示向左

    public Rigidbody2D rb;//处理所有物理效果
    public Animator anim; //动画控制器
                          

    // Update is called once per frame
    void FixedUpdate()  //改成fixedUpdate，每秒50次，保证物理效果的稳定性
    {
        float horizontal = Input.GetAxis("Horizontal");//水平 监听左右 ad 键
        float vertical = Input.GetAxis("Vertical");
        if(horizontal >0 &&transform.localScale.x<0|| horizontal < 0 && transform.localScale.x > 0) //如果水平输入大于0，且面朝方向是向左， -1则翻转 或者水平输入小于0，且面朝方向是向右，1则翻转
        {
            Flip();
        }
        

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.velocity = new Vector2(horizontal,vertical)*speed; //velocity 是速度，Vector2 是一个二维向量，表示物体在 x 和 y 方向上的速度
        //直接改速度，但是保留碰撞，相比transform直接改位置（穿模） 会有更好的物理效果

    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(facingDirection, 1, 1); //要修改整个向量
    }

}
