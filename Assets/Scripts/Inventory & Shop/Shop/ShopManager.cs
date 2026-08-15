using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static event Action<ShopManager, bool> OnShopStateChanged;
    //使用serializeField  +private 只在内部使用，但同时可以在inspector赋值修改
    [SerializeField] private List<ShopItems> shopItems;//列表可变长，每个商人可卖的东西不一定是8个
    [SerializeField] private ShopSlot[] shopSlots;// 用数组，固定

    [SerializeField] private InventoryManager inventoryManager;//库存管理器引用，检查是否有足够的金币
    private void Start()
    {
        PopulateShopItems();
        OnShopStateChanged?.Invoke(this, true);
    }
    public void PopulateShopItems()
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemSO, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);//槽位本身不能setActive ，必须获取游戏对象
        }
        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);//把不要的失活
        }
    }


    public void TryBuyItem(ItemSO itemSO, int price)
    {
        if (itemSO != null && inventoryManager.gold >= price)
        {
            if (HasSpaceForItem(itemSO))
            {
                inventoryManager.gold -= price;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();//更新金币和文本
                inventoryManager.AddItem(itemSO, 1);//填充物品
            }
        }
    }
    private bool HasSpaceForItem(ItemSO itemSO)//只做判断不填充物品
    {
        foreach (var slot in inventoryManager.itemSlots)
        {
            if (slot.itemSO == itemSO && slot.quantity <= itemSO.statckSize)
            {
                return true;
            }
            else if (slot.itemSO == null)
            {
                return true;//找到空位置
            }
        }
        return false;
    }
    public void SellItem(ItemSO itemSO)
    {
        if (itemSO == null)
        {
            return;
        }
        foreach (var slot in shopSlots)
        {
            if (slot.itemSO == itemSO)
            {
                inventoryManager.gold += slot.price;//可以添加自定义逻辑让物品价值减少
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                return;
            }
        }
    }

    //  ShopItems 是一个自定义类，不是 MonoBehaviour，如果不加 [System.Serializable]，Unity 根本不知道怎样在 Inspector 里显示它，也不知道怎样保存它的数据。
    [System.Serializable]
    public class ShopItems
    {
        public ItemSO itemSO;
        public int price;
    }
}