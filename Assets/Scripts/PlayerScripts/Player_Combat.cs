using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{

    public StatusUI statusUI;
    // Start is called before the first frame update
    public Transform attackPoint;

    public LayerMask enemyLayer;

    public Animator anim;
    public float cooldown = 1;
    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            //不要在这里调用DealDamage，   因为在进入攻击动画的一瞬间就会造成伤害，敌人销毁，所以让伤害处理延后， 添加公共方法，在动画事件里面调用
            timer = cooldown;
        }
    }
    public void DealDamage()//伤害处理
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatusManager.Instance.weaponRange, enemyLayer); //检测攻击范围内的所有敌人碰撞体
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-StatusManager.Instance.damage);  //对第一个敌人碰撞体造成伤害 ,要对所有敌人造成伤害，需要foreach循环
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, StatusManager.Instance.knockbackForce, StatusManager.Instance.knockbackTime, StatusManager.Instance.stunTime); //
        }
        StatusManager.Instance.damage += 1;
        // statusUI.UpdateAllstatus();
    }
    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()//在编辑器中显示攻击范围
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPoint.position, StatusManager.Instance.weaponRange);
    }


}
