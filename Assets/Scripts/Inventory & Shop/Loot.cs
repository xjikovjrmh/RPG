using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;
    public bool canBePickedUp = true;
    public int quantity;//数额
    public static event Action<ItemSO, int> OnItemLooted;// 创建事件，传递整数
    private void OnValidate()
    {
        if (itemSO == null)
        {
            return;
        }
        UpdateAppearance();
    }
    public void Initialize(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
        canBePickedUp = false;//不可拾取
        UpdateAppearance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBePickedUp == true)
        {
            // Debug.Log("Item pickup");
            anim.Play("LootPickup");
            OnItemLooted?.Invoke(itemSO, quantity);
            Destroy(gameObject, .5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBePickedUp = true;//
        }
    }
    private void UpdateAppearance()//更新外观
    {
        sr.sprite = itemSO.Icon;
        this.name = itemSO.itemName;
    }


}
