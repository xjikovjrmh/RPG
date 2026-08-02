using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class SkillSlot : MonoBehaviour
{
    public List<SkillSlot> PrerequisiteSkillSLots; //前置技能槽列表
    public SkillSO skillso;
    public int currentLevel;
    public bool isUnlocked;
    public Image skillIcon;
    //引用按钮
    public Button skillButton;

    public TMP_Text skillLevelText;

    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;


    private void OnValidate() //大小V 不是小写
    {
        if (skillso != null && skillLevelText != null)
        {

            UpdateUI();
        }
    }

    public void TryUpgradeSkill()//尝试升级技能
    {
        if (isUnlocked && currentLevel < skillso.maxLevel)//技能已解锁且当前等级小于最大等级 时才能加等级
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);//传递消息, 把自己作为参数
            if (currentLevel >= skillso.maxLevel)//达到上限
            {
                OnSkillMaxed?.Invoke(this);
            }
            UpdateUI();
        }
    }

    private void UpdateUI()//更新文本和颜色
    {
        skillIcon.sprite = skillso.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;//解锁技能，按钮可以点击
            skillLevelText.text = currentLevel + "/" + skillso.maxLevel;
            skillIcon.color = Color.white;//解锁技能， 设置为白色
        }
        else
        {
            skillButton.interactable = false;//锁定技能，按钮不能点击
            skillLevelText.text = "Locked";
            skillIcon.color = Color.gray;//锁定技能， 设置为灰色
        }
    }
    public bool CanUnlockSkill()//需要设置前置条件 ，用列表实现
    {
        foreach (SkillSlot slot in PrerequisiteSkillSLots)
        {
            if (!slot.isUnlocked || slot.currentLevel < slot.skillso.maxLevel)//未解锁或未升满级
            {
                return false;
            }
        }
        return true;
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }
}
