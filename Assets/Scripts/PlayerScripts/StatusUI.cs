using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] statusSlots;
    public CanvasGroup statusCanvas;

    private bool statsOpen = false;

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))// GetButtonDown 按一下触发 不是 GetButton （按下的每一帧触发）
        {
            if (statsOpen)//打开， -> 关闭
            {
                Time.timeScale = 1;
                UpdateAllstatus();
                statusCanvas.alpha = 0;//透明
                statsOpen = false;
            }
            else                  //打开
            {
                Time.timeScale = 0;
                UpdateAllstatus();
                statusCanvas.alpha = 1;
                statsOpen = true;
            }
        }
    }
    private void Start()//游戏开始时初始化
    {
        UpdateAllstatus();
    }
    public void UpdateDamage()
    {
        statusSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatusManager.Instance.damage;
    }
    public void UpdateSpeed()
    {
        statusSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatusManager.Instance.speed;
    }

    public void UpdateAllstatus()//也用于外部调用更新面板
    {
        UpdateDamage();
        UpdateSpeed();
    }

}
