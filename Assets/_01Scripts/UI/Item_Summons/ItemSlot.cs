using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    protected Image itemIcon;
    [SerializeField]
    protected Image backGround;
    [SerializeField]
    protected Image frame;
    [SerializeField]
    protected TextMeshProUGUI itemName;
    [SerializeField]
    protected TextMeshProUGUI levelText;
    [SerializeField]
    protected TextMeshProUGUI itemGrade;
    [SerializeField]
    protected TextMeshProUGUI itemPow;

    public Item item;
  
    public void Setting(Item item)
    {
        this.item = item;
        if (ItemManager.instance == null) return;
        if(item == null)
        {
            itemIcon.sprite = ItemManager.instance.defaultBackGround;
            backGround.sprite = ItemManager.instance.defaultBackGround;
            frame.sprite = ItemManager.instance.defaultSlot;
            if (itemName != null) itemName.text = null;
            if (levelText != null) levelText.text = null;
            if (itemGrade != null) itemGrade.text = null;
            if (itemPow != null) itemPow.text = null;
        }
        else
        {
            itemIcon.sprite = item.itemData.icon;
            backGround.sprite = item.backGround;
            frame.sprite = item.slot;
            if (itemName != null) itemName.text = item.itemData.itemname;
            if (levelText != null) levelText.text = string.Format("Lv.{0}", item.ItemLv);
            if (itemGrade != null) itemGrade.text = item.itemGrade.ToString();
            if (itemPow != null) itemPow.text = string.Format("stat : + {0 }", item.Cal_LevelupPow(item.ItemLv));
        }
    }

  
}
