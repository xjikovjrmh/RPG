using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 独立存在，无需挂载
// 无Update方法，只有简单生命周期  存游戏数据s

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]
public class SkillSO : ScriptableObject  //可脚本化对象 ，不能继承自monoBehaviour，不能挂在物体上，不能使用update和start等方法
{
    public string skillName;
    public int maxLevel;
    public Sprite skillIcon;
}
