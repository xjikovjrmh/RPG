using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusManager : MonoBehaviour
{
    public StatusUI statusUI;
    public static StatusManager Instance;//数值管理器实例 唯一
    public TMP_Text healthText;

    [Header("Combat Status")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Status")]
    public int speed;

    [Header("Health Status")]
    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        // currentHealth = maxHealth; //更新最大生命值时，当前生命值也设置为最大生命值
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;//TMP 会自动更新文本显示

    }
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;//TMP 会自动更新文本显示

    }
    public void UpdateSpeed(int amount)
    {
        speed += amount;
        statusUI.UpdateAllstatus();

    }
}