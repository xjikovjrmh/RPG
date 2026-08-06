using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]

public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;//让文本框变大
    public Sprite Icon;

    public bool isGold;
    [Header("Status")]

    public int CurrentHealth;
    public int MaxHealth;
    public int Speed;
    public int Damage;
    [Header("For Temporary Items")]
    public float Duration;


}
