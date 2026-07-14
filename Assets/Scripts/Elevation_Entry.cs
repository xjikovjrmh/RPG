using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Entry : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D[] mountainColliders;
    public Collider2D[] boundartColliders;
    private void OnTriggerEnter2D(Collider2D collision)// 一旦进入2D碰撞器触发器时，启用山脉碰撞器
    {
        if(collision.gameObject.tag=="Player")//加上限制 检测物体标签
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = false;//关闭
            }
            foreach(Collider2D boundary in boundartColliders)
            {
                boundary.enabled = true;//进入山脉开启
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;//提高玩家层级
        }
        

    }




}
