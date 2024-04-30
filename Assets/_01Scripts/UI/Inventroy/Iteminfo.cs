using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteminfo : MonoBehaviour
{
    [SerializeField]
    GameObject infoPanel;
    [SerializeField]
    ItemSlot itemInfoSlot;
    [SerializeField]
    GameObject changeItempPanel;
    [SerializeField]
    ItemSlot equiopedItemSlot;
    [SerializeField]
    ItemSlot thisItemSlot;

    [SerializeField]
    GameObject enhancementPanel;
    [SerializeField]
    EnhancementItemSlot enhancementSlot;

    [SerializeField]
    EquipItem equipItem;

    public Item equipeditem;
    Item item;

    public void Setting(Item item)
    {
        changeItempPanel.SetActive(false);
        enhancementPanel.SetActive(false);
        this.item = item;
        itemInfoSlot.Setting(item);
        infoPanel.SetActive(true); 
    }

    public void EquipButton()
    {
        equipeditem = equipItem.OutEquipItem(item.itemData.itemType);
          
        equiopedItemSlot.Setting(equipeditem);
        thisItemSlot.Setting(item);
        changeItempPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void OnEquip()
    {
        equipItem.EquipItems(item);
        changeItempPanel.SetActive(false);
        infoPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void CancelEquip()
    {
        changeItempPanel.SetActive(false);
        infoPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void EnhancementButton()
    {
        enhancementSlot.Setting(item);
        enhancementSlot.Settingother(item);
        enhancementPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }
}
