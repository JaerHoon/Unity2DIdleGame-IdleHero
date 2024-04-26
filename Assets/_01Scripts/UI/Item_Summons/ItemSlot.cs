using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    Image itemIcon;
    [SerializeField]
    Image backGround;
    [SerializeField]
    Image slot;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI levelText;
    
  

    public void Setting(Item item)
    {
        if (ItemManager.instance == null) return;
        if(item == null)
        {
            itemIcon.sprite = ItemManager.instance.defaultBackGround;
            backGround.sprite = ItemManager.instance.defaultBackGround;
            slot.sprite = ItemManager.instance.defaultSlot;
            if (itemName != null) itemName.text = null;
            if (levelText != null) levelText.text = null;
        }
        else
        {
            itemIcon.sprite = item.itemData.icon;
            backGround.sprite = item.backGround;
            slot.sprite = item.slot;
            if (itemName != null) itemName.text = item.itemData.itemname;
            if (levelText != null) levelText.text = string.Format("Lv.{0}", item.ItemLv);
        }

        
    }
}
