using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //使用serializeField  +private 只在内部使用，但同时可以在inspector赋值修改
    [SerializeField] private List<ShopItems> shopItems;//列表可变长，每个商人可卖的东西不一定是8个
    [SerializeField] private ShopSlot[] shopSlots;// 用数组，固定


    private void Start()
    {
        PopulateShopItems();
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
}

//  ShopItems 是一个自定义类，不是 MonoBehaviour，如果不加 [System.Serializable]，Unity 根本不知道怎样在 Inspector 里显示它，也不知道怎样保存它的数据。
[System.Serializable]
public class ShopItems
{
    public ItemSO itemSO;
    public int price;
}
