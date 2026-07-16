using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eneny_Combat : MonoBehaviour
{
    public int damage = 1; //方便回溯定位，不要直接传入数字1

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)  //当敌人与另一个2D碰撞体发生碰撞时调用
    {
        
        if (collision.gameObject.CompareTag("Player"))  //如果碰撞的对象是玩家
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);  //获取玩家脚本组件
            
        }
    }


}
