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
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }
    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(slot.TryUpgradeSkill);
        }
        UpdateAbilityPoints(0);
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
    public void UpdateAbilityPoints(int amount)
    {
        availablePoints += amount;
        PointsText.text = "Points: " + availablePoints;
    }


}
