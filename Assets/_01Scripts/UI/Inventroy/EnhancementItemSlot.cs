using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class EnhancementItemSlot : Slots
{
    [SerializeField]
    TextMeshProUGUI afterItemLV;
    [SerializeField]
    TextMeshProUGUI afterItemPow;
    [SerializeField]
    TextMeshProUGUI enhancementRate;
    [SerializeField]
    TextMeshProUGUI enhancementCost;

    [SerializeField]
    GameObject OnEnhancementPanel;
    [SerializeField]
    GameObject successPanel;
    [SerializeField]
    GameObject failPanel;
    [SerializeField]
    Image successIcon;
    [SerializeField]
    Image successBack;
    [SerializeField]
    Image successFrame;
    [SerializeField]
    TextMeshProUGUI successLv;
    [SerializeField]
    TextMeshProUGUI successpow;

    private void OnEnable()
    {
        OnEnhancementPanel.SetActive(false);
    }

    public void Settingother(Item item)
    {
        this.item = item;
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
     
    public bool Cal_enhancement(Item item)
    {
        int[] num = Utility.RandomCreate(1, 0, 100);

        bool isenhance = num[0] < Cal_Rate(item);

        return isenhance;  
    }

    public void Onenhancement(Item item)
    {
        OnEnhancementPanel.SetActive(true);
        
        if (Cal_enhancement(item))
        {
            item.ItemLv ++;
            successBack.sprite = item.backGround;
            successFrame.sprite = item.slot;
            successIcon.sprite = item.itemData.icon;
            successLv. text = string.Format("Lv.{0}", item.ItemLv);
            successpow.text = string.Format("stat : + {0}", item.Cal_LevelupPow(item.ItemLv));
            successPanel.SetActive(true);
            failPanel.SetActive(false); 
        }
        else
        {
            successPanel.SetActive(false);
            failPanel.SetActive(true);
        }
    }
}
