using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int[] stat_Lv;
    public List<Item> items;
    public List<Item> gained_Items;
    public Item[] equiped_Item;
    public int gold;
    public int jewel;
    public int stage;
}
