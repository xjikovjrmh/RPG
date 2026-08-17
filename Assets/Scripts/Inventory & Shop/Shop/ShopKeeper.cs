using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour
{
    public static ShopKeeper currentShopKeeper;
    public Animator anim;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;
    //由商人管理
    [SerializeField] private List<ShopItems> shopItems;//列表可变长，每个商人可卖的东西不一定是8个
    [SerializeField] private List<ShopItems> shopWeapons;
    [SerializeField] private List<ShopItems> shopArmours;
    public static event Action<ShopManager, bool> OnShopStateChanged;//状态改变
    private bool playerInRange;
    private bool isShopOpen = false;

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)//商店未打开->开启商店
                {
                    currentShopKeeper = this;//设置为当前店主
                    isShopOpen = true;
                    Time.timeScale = 0;
                    OnShopStateChanged?.Invoke(shopManager, true);
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;
                    OpenItemShop();//默认打开物品商店
                }
            }
            else if (Input.GetButtonDown("Cancel")) //可以用esc取消
            {
                currentShopKeeper = null;
                isShopOpen = false;
                Time.timeScale = 1;
                OnShopStateChanged?.Invoke(shopManager, false);
                shopCanvasGroup.alpha = 0;
                shopCanvasGroup.blocksRaycasts = false;
                shopCanvasGroup.interactable = false;
            }
        }
    }


    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);
    }
    public void OpenWeaponShop()//武器店
    {
        shopManager.PopulateShopItems(shopWeapons);
    }
    public void OpenArmourShop()//防具店
    {
        shopManager.PopulateShopItems(shopArmours);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", false);
            playerInRange = false;
            //从播放动画到Idle的过渡设置退出时间为1可以让动画播放完整
        }
    }
}
