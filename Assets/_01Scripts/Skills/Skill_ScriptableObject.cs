using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill Data", menuName = "ScriptableObject/Skill Data")]
public class Skill_ScriptableObject : ScriptableObject
{
    public int skillNumber;
    public float damage;
    public float coolTime;
    public string skillname;
    public string skillinfo;
    public GameObject skillPrefab;
    public Sprite icon;
    
  

}
