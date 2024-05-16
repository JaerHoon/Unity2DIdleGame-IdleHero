using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int[] stat_Lv = new int[4];
    public Item[] equiped_Item = new Item[6];
    public List<Item> items = new List<Item>();
    public List<Item> gained_Items = new List<Item>();
    public int gold;
    public int jewel;
    public int stage;
}
