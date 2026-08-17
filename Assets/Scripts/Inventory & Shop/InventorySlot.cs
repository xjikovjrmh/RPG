using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itemSO;
    public int quantity;
    public Image itemImage;
    public TMP_Text quantityText;

    private InventoryManager inventoryManager;
    private static ShopManager activeShop;//对活跃商店的引用（用于出售物品）,static 所有库存槽位共享同一引用
    public void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }
    private void OnEnable()//漏写字母
    {
        //修改为商人，该事件由商人发送
        ShopKeeper.OnShopStateChanged += HandleShopStateChanged;
    }
    private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChanged;
    }
    private void HandleShopStateChanged(ShopManager shopManager, bool isOpen)
    {
        activeShop = isOpen ? shopManager : null;//true 这传递shopManager ，false 则传递空值
        Debug.Log(isOpen);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)//左键使用
            {
                if (activeShop != null)//商店开了，左键出售
                {

                    activeShop.SellItem(itemSO);
                    quantity--;
                    UpdateUI();
                }
                else //商店没开，左键使用
                {
                    //已满血不使用治疗物品
                    if (itemSO.currentHealth > 0 && StatusManager.Instance.currentHealth >= StatusManager.Instance.maxHealth)
                        return;
                    inventoryManager.UseItem(this);
                }
            }

            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }

    }
    public void UpdateUI()
    {
        if (quantity <= 0)
        {
            itemSO = null;//清除脚本化对象，在出售完物品时才会清空
        }
        if (itemSO != null)
        {
            itemImage.sprite = itemSO.Icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else//没有物品
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
}
