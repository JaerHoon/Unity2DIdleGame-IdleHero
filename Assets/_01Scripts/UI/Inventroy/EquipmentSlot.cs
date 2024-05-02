using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : Slots
{
    public enum SlotType { Weapon, Shield, Helmet, Armor, Shoes, Accessories }
    public SlotType slotType;

    [SerializeField]
    EquipmentItem equipmentItem;

    Item equitedItem;

    private void OnEnable()
    {
        EquipItem(equitedItem);
    }

    void NullSetting()
    {
        if (ItemManager.instance== null) return;
        itemIcon.sprite = ItemManager.instance.defaultItemIcon[(int)slotType];
        itemIcon.color = Color.black;
        backGround.sprite = ItemManager.instance.defaultBackGround;
        frame.sprite = ItemManager.instance.defaultSlot;

        if (itemName != null) itemName.text = null;
        if (levelText != null) levelText.text = null;
        if (itemGrade != null) itemGrade.text = null;
        if (itemPow != null) itemPow.text = null;

    }

    public void EquipItem(Item item)
    {
        if(item != null)
        {
            equitedItem = item;
            itemIcon.color = Color.white;
            Setting(item);
        }
        else
        {
            NullSetting();
        }
    }

    public void Onclick()
    {
       if(equitedItem!=null)
        {
            equipmentItem.OnItemInfo(item);
        }
        else
        {
            return;
        }
    }

    
}
