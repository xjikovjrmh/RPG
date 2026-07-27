using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eneny_Combat : MonoBehaviour
{
    public int damage = 1; //方便回溯定位，不要直接传入数字1
    public Transform attackPoint; //放在武器位置
    public float weaponRange; //武器攻击范围
    public LayerMask playerLayer; //更高效的检测
    public float stunTime;
    public float force;
    // Start is called before the first frame update
    //private void OnCollisionEnter2D(Collision2D collision)  //当敌人与另一个2D碰撞体发生碰撞时调用
    //{
        
    //    if (collision.gameObject.CompareTag("Player"))  //如果碰撞的对象是玩家
    //    {
    //        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);  //获取玩家脚本组件
            
    //    }
    //}

    public void Attack()
    {

        Collider2D[] hits =Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer); //检测攻击范围内的所有玩家碰撞体
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);  //对第一个玩家碰撞体造成伤害
            hits[0].GetComponent<PlayerMovement>().Knockback(transform,force,stunTime); //击退玩家   可以多多使用此方法来寻找脚本
        }
    }

}
