using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 2;  //存在时间
    public float speed;

    public int damage;    //给每种箭单独创建数值，方便管理扩展更多样的箭
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    public LayerMask enemyLayer; //
    public LayerMask obstacleLayer;  //障碍物层

    public SpriteRenderer sr;//引用当前箭矢的精灵图  后续替换
    public Sprite buriedSprite; //埋入地面后的箭矢贴图

    void Start()
    {
        rb.velocity = direction * speed;
        RotateArrow();
        Destroy(gameObject, lifeSpawn); //在lifeSpawn秒后销毁箭矢
    }
    private void RotateArrow()
    {                   //计算弧度，                            换算成度数    
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //人物在xoy平面移动需绕z轴旋转  给刚体锁定z轴旋转 即可保证射出的箭不旋转
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer.value) > 0)//检查碰撞的对象是否在敌人图层中
        {
            collision.gameObject.GetComponent<Enemy_Health>().ChangeHealth(-damage);
            collision.gameObject.GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime); //调用击退方法，传入玩家位置，击退力，击退时间和眩晕时间
            AttachToTarget(collision.gameObject.transform);//不能给物体同时设置多个层级
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }


    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite; //更换箭矢贴图为埋入地面后的箭矢贴图
        rb.velocity = Vector2.zero; //停止箭矢移动
        rb.isKinematic = true; //将刚体设置为静态，防止物理影响
        transform.SetParent(target); //将箭矢设置为目标的子物体，使其随目标移动
    }
}
