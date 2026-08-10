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
    public void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)//左键使用
            {
                //已满血不使用治疗物品
                if (itemSO.currentHealth > 0 && StatusManager.Instance.currentHealth >= StatusManager.Instance.maxHealth) return;
                inventoryManager.UseItem(this);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }

    }
    public void UpdateUI()
    {
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
