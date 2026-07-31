using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        StatusManager.Instance.currentHealth = StatusManager.Instance.maxHealth;
        healthText.text = "HP: " + StatusManager.Instance.currentHealth + "/" + StatusManager.Instance.maxHealth;
    }

    public void ChangeHealth(int amount)  //正代表治疗，负数代表受伤
    {
        StatusManager.Instance.currentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + StatusManager.Instance.currentHealth + "/" + StatusManager.Instance.maxHealth;
        if (StatusManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
