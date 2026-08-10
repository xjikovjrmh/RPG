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
    public int statckSize = 3;//堆叠上限
    [Header("Status")]

    public int currentHealth;
    public int maxHealth;
    public int speed;
    public int damage;
    [Header("For Temporary Items")]
    public float duration;


}
