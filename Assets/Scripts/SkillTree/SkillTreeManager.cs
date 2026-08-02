using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TMP_Text PointsText;
    public int availablePoints;

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateAbilityPoints; //订阅等级提升事件
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateAbilityPoints; //取消订阅等级提升事件
    }
    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {                                                                       //之前tryUpgradeSkill()方法不会检查是否有可用点数
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoints(slot));//这个监听器会传递slot参数给CheckAvailablePoints方法
        }
        UpdateAbilityPoints(0);
    }
    private void CheckAvailablePoints(SkillSlot slot)
    {
        if (availablePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
        else
        {
            Debug.Log("No available points to spend.");
        }
    }

    private void HandleAbilityPointSpent(SkillSlot skillSlot)
    {
        if (availablePoints > 0)
        {

            UpdateAbilityPoints(-1);
        }
        else
        {
            Debug.Log("No available points to spend.");
        }
    }
    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
                slot.Unlock();
        }
    }

    public void UpdateAbilityPoints(int amount)
    {
        availablePoints += amount;
        PointsText.text = "Points: " + availablePoints;
    }


}
