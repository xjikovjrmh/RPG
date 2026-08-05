using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemyMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<Enemy_Movement>();
    }
    // Start is called before the first frame update
    public void Knockback(Transform ForceTransform, float force, float knockbacktime, float stuntime)
    {
        enemyMovement.ChangeState(EnemyState.Knockback); //改变敌人状态为击退状态
        StartCoroutine(StunTimer(knockbacktime, stuntime));

        Vector2 direction = (transform.position - ForceTransform.position).normalized; //计算击退方向
        rb.velocity = direction * force; //施加击退力


        Debug.Log("Knockback applied to enemy");
    }

    private IEnumerator StunTimer(float knockbacktime, float stunTime)
    {
        yield return new WaitForSeconds(knockbacktime);//击退时间结束
        rb.velocity = Vector2.zero;  //zero 是小写
        yield return new WaitForSeconds(stunTime);// 眩晕时间
        //要记得切换状态
        enemyMovement.ChangeState(EnemyState.Idle);
    }

}
