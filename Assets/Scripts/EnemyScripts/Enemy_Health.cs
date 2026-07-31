using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{

    public int ExpReward = 3;
    public delegate void MonsterDefeated(int exp); //创建委托(存储事件)，传递整数
    public static event MonsterDefeated OnMonsterDefeated;//静态，全局消息中心， 怪物死了，像所有订阅了事件的地方发送消息。不需要拖拽
    //但是静态事件不会被垃圾回收，必须在OnDisable方法里面取消订阅

    // Start is called before the first frame update
    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

    }
    private void Die()
    {
        OnMonsterDefeated(ExpReward);
        Destroy(gameObject);
    }


}
