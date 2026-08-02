using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogleSkillTree : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup statsCanvas;
    private bool isSkillTreeOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (isSkillTreeOpen)//已经打开
            {
                Time.timeScale = 1f; //恢复游戏时间
                statsCanvas.alpha = 0;
                // statsCanvas.interactable = false; //不需要， 因为 blockRaycasts 会让射线穿透按钮，根本点击不了
                statsCanvas.blocksRaycasts = false; //不再阻挡射线
                isSkillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0f; //暂停游戏时间
                statsCanvas.alpha = 1;
                // statsCanvas.interactable = true;
                statsCanvas.blocksRaycasts = true;
                isSkillTreeOpen = true;
            }
        }
    }
}
