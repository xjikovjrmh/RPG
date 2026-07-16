using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D[] mountainColliders;
    public Collider2D[] boundartColliders;
    private void OnTriggerEnter2D(Collider2D collision)// 一旦进入2D碰撞器触发器时，
    {
        if (collision.gameObject.tag == "Player")//加上限制 检测物体标签
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;//启用
            }
            foreach (Collider2D boundary in boundartColliders)
            {
                boundary.enabled = false;//离开山脉开启
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;//降低玩家层级  不能是10
        }


    }


}
