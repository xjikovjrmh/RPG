using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public ItemSO itemSO;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Image itemImage;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private ShopInfo shopInfo;

    public int price;
    public void Initialize(ItemSO newItemSO, int price)//newItemSO 是传入的物品 
    {
        itemSO = newItemSO;
        itemImage.sprite = itemSO.Icon;
        itemNameText.text = itemSO.itemName;
        this.price = price;
        priceText.text = price.ToString();
    }
    public void OnBuyButtonClicked()
    {
        shopManager.TryBuyItem(itemSO, price);
    }
    //     实现接口时，方法名必须和接口定义完全一致，不能自己改名。

    // 接口	方法名
    // IPointerEnterHandler	OnPointerEnter
    // IPointerClickHandler	OnPointerClick
    // IPointerExitHandler	OnPointerExit
    // IPointerDownHandler	OnPointerDown
    // IPointerUpHandler	OnPointerUp
    public void OnPointerEnter(PointerEventData eventDate)//鼠标制作进入物体射线检测区域时触发
    {
        if (itemSO != null)
            shopInfo.showItemInfo(itemSO);//鼠标进入，开始显示
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        shopInfo.HideItemInfo();
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        if (itemSO != null)
            shopInfo.FollowMouse();//跟随鼠标 //注意infoPanel 的pivot要设置成 0,1 即左上角为原点， 如果是0.5,0.5 鼠标会在面板中心
    }
}
