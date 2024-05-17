using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int[] stat_Lv = new int[4];
    public ItemSaveData[] equiped_Item = new ItemSaveData[6];
    public List<ItemSaveData> items = new List<ItemSaveData>();
    public List<ItemSaveData> gained_Items = new List<ItemSaveData>();
    public int gold;
    public int jewel;
    public int stage;
    public int[] skillSlot = new int[3];
    public int ActiveQuestNum;

    public void Init (List<ItemSaveData> itemList, List<ItemSaveData> gainedList, ItemSaveData[] equipList)
    {
        items = itemList;
        gained_Items = gainedList;
        equiped_Item = equipList;
    }

}

[System.Serializable]
public class ItemSaveData
{
    public Item_ScriptableObject itemData;
    [SerializeField]
    int itemLV;
    public  Item.ItemGrade itemGrade;
    public Sprite backGround;
    public Sprite slot;
    public string statUPType;

    public ItemSaveData(Item item)
    {
        if(item != null)
        {
            itemData = item.itemData;
            itemLV = item.ItemLv;
            itemGrade = item.itemGrade;
            backGround = item.backGround;
            slot = item.slot;
            statUPType = item.statUPType;
        }
        else
        {
            statUPType = "";
        }
       
    }

    public Item OutData()
    {
        if(statUPType != "")
        {
            Item item = new Item();
            item.itemData = itemData;
            item.Setting(backGround, slot);
            item.ItemLv = itemLV;
            item.itemGrade = itemGrade;

            return item;
        }
        else
        {
            return null;
        }
      
    }
}