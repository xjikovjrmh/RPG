using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int facingDirection = 1; //记录面朝方向 1表示向右，-1表示向左

    public Rigidbody2D rb;//处理所有物理效果
    public Animator anim; //动画控制器

    private bool isKonckedBack;
    public bool isShooting;

    public Player_Combat player_Combat;//与攻击脚本通信， 处理移动到攻击的过渡

    void Update()//获取输入反馈最快，
    {
        if (Input.GetButtonDown("Slash") && player_Combat.enabled == true) // k //要添加攻击检查，防止锁定情况下攻击
        {

            player_Combat.Attack();
        }
    }


    // Update is called once per frame
    void FixedUpdate()  //改成fixedUpdate，每秒50次，保证物理效果的稳定性
    {
        if (isShooting == true) //如果在射击， 也不处理移动    当然，需要切换bool变量开关，当射击完成后 变化，这需要与player_Bow通信
        {
            rb.velocity = Vector2.zero;
        }
        else if (isKonckedBack == false)  //如果被击退，则不处理移动逻辑
        {
            float horizontal = Input.GetAxis("Horizontal");//水平 监听左右 ad 键
            float vertical = Input.GetAxis("Vertical");
            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0) //如果水平输入大于0，且面朝方向是向左， -1则翻转 或者水平输入小于0，且面朝方向是向右，1则翻转
            {
                Flip();
            }


            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            rb.velocity = new Vector2(horizontal, vertical) * StatusManager.Instance.speed; //velocity 是速度，Vector2 是一个二维向量，表示物体在 x 和 y 方向上的速度
                                                                                            //直接改速度，但是保留碰撞，相比transform直接改位置（穿模） 会有更好的物理效果
                                                                                            //不能再两个脚本里面同时改速度，会覆盖
        }


    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(facingDirection, 1, 1); //要修改整个向量
    }

    //养成好习惯，方法首字符大写，便于区分变量和方法
    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKonckedBack = true;
        Vector2 knockbackDirection = (transform.position - enemy.position).normalized; //击退方向，单位向量 归一化
        rb.velocity = knockbackDirection * force;
        //S  首字母大写，表示协程，返回类型是IEnumerator
        StartCoroutine(KnockbackConuter(stunTime)); //协程，等待击退时间结束

    }
    private IEnumerator KnockbackConuter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        isKonckedBack = false; //击退结束，恢复移动
    }

}
