using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data")]

public class Item_ScriptableObject : ScriptableObject
{
    public enum ItemType { Weapon, Shield, Helmet, Armor, Shoes, Accessories}
    public ItemType itemType;

    public string itemname;
    public string koreanName;
    public Sprite icon;
    public int itemPow;//기본적인 아이템 스탯 업 효과
    public int LvUP_Rate;
    
}
