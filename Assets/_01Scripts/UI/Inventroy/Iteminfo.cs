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
        Item equipment = ItemManager.instance.equipments[(int)item.itemData.itemType];
        equiopedItemSlot.Setting(equipment);
        thisItemSlot.Setting(item);
        changeItempPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void OnEquip()
    {
        ItemManager.instance.OnEquipItem(item);
        changeItempPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
    
  

    public void EnhancementButton()
    {
        enhancementSlot.Setting(item);
        enhancementSlot.Settingother(item);
        enhancementPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void OnEnhancement()
    {
        //금화가 있다면 충분하다면 추가요
        if (enhancementSlot.Cal_enhancement(item))
        {
            item.ItemLv++;
            ItemManager.instance.ChangeEqument.Invoke();
            this.gameObject.SetActive(false);
        }
       
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }
}
