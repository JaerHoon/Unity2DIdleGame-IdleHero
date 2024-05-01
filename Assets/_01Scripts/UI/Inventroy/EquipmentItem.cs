using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    [SerializeField]
    List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();

    public void EquipItem(Item item)
    {
        equipmentSlots[(int)item.itemData.itemType].EquipItem(item);
    }

    public Item GetItem(Item_ScriptableObject.ItemType itemType)
    {
        Item item = equipmentSlots[(int)itemType].GetItem();

        return item;
    }
}
