using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player_Combat playerCombat;//引用Player_Combat脚本

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent; //因为这个事件会传递一个SkillSlot参数，所以可以用来知道是哪个技能
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }
    private void HandleAbilityPointSpent(SkillSlot slot)//每消耗一个点数都会调用这个方法
    {
        string skillName = slot.skillso.skillName;
        switch (skillName)
        {
            case "Max Health Boost":
                StatusManager.Instance.UpdateMaxHealth(1);
                break;
            case "Sword Slash":
                playerCombat.enabled = true;
                break;

            default:
                Debug.LogWarning("Unhandled skill: " + skillName);//调试未知技能，防止输错
                break;
        }


    }
}
