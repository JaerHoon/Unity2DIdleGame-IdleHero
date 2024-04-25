using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill Data", menuName = "ScriptableObject/Skill Data")]
public class Skill_ScriptableObject : ScriptableObject
{
    public float damage;
    public float coolTime;

    public string animationName;
    public Sprite icon;
}
