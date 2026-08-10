using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int gold;
    public TMP_Text goldText;
    public InventorySlot[] itemSlots;
    public UseItem useItem;
    public GameObject LootPrefab;
    public Transform player;
    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUI();//初始更新槽位，如果为空，则
        }
    }
    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;  //不能加括号，AddItem()是方法的结果，AddItem才是方法
    }
    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }
    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)//是否为金币
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }
        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == itemSO && slot.quantity < itemSO.statckSize)//分配到已经存放了相同物品的格子
            {
                int availableSpace = itemSO.statckSize - slot.quantity;
                int amountToAdd = Mathf.Min(availableSpace, quantity);
                slot.quantity += amountToAdd;
                quantity -= amountToAdd;//场景中数量减少

                slot.UpdateUI();
                if (quantity <= 0) //如果物品已经分配完直接退出
                    return;
            }
        }
        foreach (var slot in itemSlots)  //如果物品还没分配完，找剩余的空格子再分配剩余物品
        {
            if (slot.itemSO == null)//只找空的那个
            {
                int amountToAdd = Mathf.Min(itemSO.statckSize, quantity);
                quantity -= amountToAdd;
                slot.itemSO = itemSO;
                slot.quantity = amountToAdd;//
                slot.UpdateUI();

                if (quantity <= 0)
                    return;//找到即终止
            }
        }
        //完全没有空间了
        if (quantity > 0)
        {
            DropLoot(itemSO, quantity);
        }


    }
    public void DropItem(InventorySlot slot)
    {
        DropLoot(slot.itemSO, 1);//丢弃槽位上1个物品
        slot.quantity--;
        if (slot.quantity <= 0)
        {
            slot.itemSO = null;
        }
        slot.UpdateUI();//更新为空槽位
    }
    private void DropLoot(ItemSO itemSO, int quantity)
    {
        Loot loot = Instantiate(LootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO, quantity);//初始化
    }

    public void UseItem(InventorySlot slot)
    {
        if (slot.itemSO != null && slot.quantity >= 0)
        {
            useItem.ApplyItemEffects(slot.itemSO);

            slot.quantity--;
            if (slot.quantity <= 0)
            {
                slot.itemSO = null;
            }
            slot.UpdateUI();
        }

    }
}
