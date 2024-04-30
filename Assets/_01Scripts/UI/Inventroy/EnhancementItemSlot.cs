using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnhancementItemSlot : ItemSlot
{
    [SerializeField]
    TextMeshProUGUI afterItemLV;
    [SerializeField]
    TextMeshProUGUI afterItemPow;
    [SerializeField]
    TextMeshProUGUI enhancementRate;
    [SerializeField]
    TextMeshProUGUI enhancementCost;


    public void Settingother(Item item)
    {
        afterItemLV.text = string.Format("Lv.{0}", item.ItemLv + 1);
        afterItemPow.text = string.Format("stat : + {0}", item.Cal_LevelupPow(item.ItemLv + 1));
        enhancementRate.text = String.Format("°­È­È®·ü : {0}%", Cal_Rate(item));
        enhancementCost.text = String.Format("{0}", Cal_Cost(item));
    }

    int Cal_Rate(Item item)
    {
        int rate = 100 - ((item.ItemLv - 1)*10);
        return rate;
    }

    int Cal_Cost(Item item)
    {
        int cost = item.ItemLv * 200 * ((int)item.itemGrade+1);
        return cost;
    }
}
