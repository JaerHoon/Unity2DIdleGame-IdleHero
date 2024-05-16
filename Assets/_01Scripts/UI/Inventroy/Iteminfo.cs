using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteminfo : MonoBehaviour
{
    [SerializeField]
    Inventroy inventroy;
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
        itemInfoSlot.statSetting(item);
        infoPanel.SetActive(true); 
    }

    public void EquipButton()
    {
        UISound.instance.PlayerSound(UISound.pressButton);
        if (itemInfoSlot.slotStat == ItemSlot.SlotStat.BeforEquip)
        {   
            Item equipment = ItemManager.instance.equipments[(int)item.itemData.itemType];
            if(equipment != null)
            {
                equiopedItemSlot.Setting(equipment);
                thisItemSlot.Setting(item);
                changeItempPanel.SetActive(true);
                infoPanel.SetActive(false);
            }
            else
            {
                OnEquip();
            }
           
        }
        else
        {
            ItemManager.instance.equipments[(int)item.itemData.itemType] = null;
            ItemManager.instance.ChangeEqument?.Invoke();
            this.gameObject.SetActive(false);
        }
        
    }

    public void OnEquip()
    {
        UISound.instance.PlayerSound(UISound.pressEquipItem);
        ItemManager.instance.OnEquipItem(item);
        changeItempPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
    
  

    public void EnhancementButton()
    {
        UISound.instance.PlayerSound(UISound.pressButton);
        enhancementSlot.Setting(item);
        enhancementSlot.Settingother(item);
        enhancementPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void OnEnhancement()
    {
        if(Resource.instance.coinNum >= enhancementSlot.Cal_Cost(item))
        {
            Resource.instance.SubResource(enhancementSlot.Cal_Cost(item), 0);
            enhancementSlot.Onenhancement(item);
           
        }
        else
        {
            return;
        }
        
       
    }

    public void Cancel()
    {
        UISound.instance.PlayerSound(UISound.pressButton);
        this.gameObject.SetActive(false);
        inventroy.OnClick((int)item.itemData.itemType);
    }
}
