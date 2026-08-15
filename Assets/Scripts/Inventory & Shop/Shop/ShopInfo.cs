using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;//控制信息面板透明度   
    public TMP_Text itemNameText;
    public TMP_Text itemDescription;
    [Header("Stat Fields")]
    public TMP_Text[] statTexts;
    private RectTransform infoPanelRect;//UI的transform面板

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }
    public void showItemInfo(ItemSO itemSO)
    {
        //开始时可以默认关闭信息面板
        infoPanel.alpha = 1;
        itemNameText.text = itemSO.itemName;
        itemDescription.text = itemSO.itemDescription;
        List<string> stats = new List<string>();//列表可以在运行时添加和移除
        //可以用有属性的抽象类来简化 这里为了入门先笨拙一点
        if (itemSO.currentHealth > 0) stats.Add("Health: " + itemSO.currentHealth.ToString());
        if (itemSO.maxHealth > 0) stats.Add("MaxHealth: " + itemSO.maxHealth.ToString());
        if (itemSO.damage > 0) stats.Add("Damage: " + itemSO.damage.ToString());
        if (itemSO.speed > 0) stats.Add("Speed: " + itemSO.speed.ToString());
        if (itemSO.duration > 0) stats.Add("Duration: " + itemSO.duration.ToString());
        if (stats.Count <= 0)
            return; //物品没有属性则退出
        for (int i = 0; i < statTexts.Length; i++)
        {
            if (i < stats.Count)//接下来将已有属性赋值给槽位
            {
                statTexts[i].text = stats[i];
                statTexts[i].gameObject.SetActive(true);

            }
            else
            {
                statTexts[i].gameObject.SetActive(false);//关掉状态物品，即不显示多的无效信息
            }
        }


    }
    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = null;
        itemDescription.text = null;

    }
    public void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 offset = new Vector3(10, -10, 0);
        infoPanelRect.position = mousePosition + offset;//鼠标位置+右下方偏移
    }

}
