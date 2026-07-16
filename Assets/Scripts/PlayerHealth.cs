using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHealth;
    public int maxHealth;
    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "HP: "+currentHealth+"/"+maxHealth;
    }

    public void ChangeHealth(int amount)  //正代表治疗，负数代表受伤
    {
        currentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        if (currentHealth<=0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
