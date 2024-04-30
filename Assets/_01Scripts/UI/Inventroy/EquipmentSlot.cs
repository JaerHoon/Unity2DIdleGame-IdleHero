using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public enum SlotType { Weapon, Shield, Helmet, Armor, Shoes, Accessories }
    public SlotType slotType;

    [SerializeField]
    EquipItem equipItemPanel;

    Item equipItem;

    public Item Equipitem { get { return equipItem; } }

    private void OnEnable()
    {
        nullSetting();
    }

    public void EquipSetting(Item item)
    {
        itemIcon.color = Color.white;
        equipItem = item;
        Setting(item);
    }

    public void nullSetting()
    {
        if (ItemManager.instance == null) return;
        if(equipItem == null)
        {
            itemIcon.sprite = ItemManager.instance.equipDefaultItem[(int)slotType];
            itemIcon.color = Color.black;
            backGround.sprite = ItemManager.instance.defaultBackGround;
            frame.sprite = ItemManager.instance.defaultSlot;
            if (itemName != null) itemName.text = null;
            if (levelText != null) levelText.text = null;
            if (itemGrade != null) itemGrade.text = null;
            if (itemPow != null) itemPow.text = null;
        }
        else
        {
            itemIcon.color = Color.white;
            Setting(equipItem);
        }
    }

    public void Onclick()
    {
        if (equipItem == null) return;
        equipItemPanel.EquipmentInfo(equipItem);
    }

   
}
