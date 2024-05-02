using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : Slots
{
    public enum SlotStat { BeforEquip, AfterEquip}
    public SlotStat slotStat;

    [SerializeField]
    TextMeshProUGUI runButtonText;
    [SerializeField]
    Iteminfo iteminfo;

    
    public void statSetting(Item item)
    {
        slotStat = SlotStat.BeforEquip;
        runButtonText.text = "장비장착";

        for (int i = 0; i < ItemManager.instance.equipments.Length;i++)
        {
            if(ItemManager.instance.equipments[i] == item)
            {
                slotStat = SlotStat.AfterEquip;
                runButtonText.text = "장비해제";
                break;
            }
        }

        
    }

  
}
