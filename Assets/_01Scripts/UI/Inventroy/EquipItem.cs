using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    [SerializeField]
    List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
    [SerializeField]
    Iteminfo iteminfo;

    public void EquipItems(Item item)
    {
        equipmentSlots[(int)item.itemData.itemType].EquipSetting(item);
    }

    public void EquipmentInfo(Item item)
    {
        iteminfo.Setting(item);
        iteminfo.gameObject.SetActive(true);
    }

    public Item OutEquipItem(Item_ScriptableObject.ItemType itemType)
    {
        return equipmentSlots[(int)itemType].Equipitem;
    }
}
