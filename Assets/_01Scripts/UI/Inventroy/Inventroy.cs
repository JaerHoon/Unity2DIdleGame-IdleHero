using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class Inventroy : MonoBehaviour
{
    
    [SerializeField]
    List<Slots> itemSlots = new List<Slots>();
    [SerializeField]
    List<Image> buttonIcons = new List<Image>();
    [SerializeField]
    List<Image> buttons = new List<Image>();

    [SerializeField]
    Sprite defaultButtonImgae;
    [SerializeField]
    Sprite choiceButtonImage;

    [SerializeField]
    Iteminfo iteminfo;


    private void OnEnable()
    {
        OnClick(0);
        iteminfo.gameObject.SetActive(false);
        //ReSetting();
    }


    private void ReSetting(int num)
    {
        Item item = null;
        for(int i=0; i < itemSlots.Count; i++)
        {
            itemSlots[i].Setting(item);
        }
        
        Setting(num);
    }

    void Setting(int num)
    {
        List<Item> item = new List<Item>();
        int eVaues = Enum.GetValues(typeof(Item_ScriptableObject.ItemType)).Length;

        if (ItemManager.instance == null) return;
        var gainitem = ItemManager.instance.gainedItems
                      .Where(n => n.itemData.itemType == (Item_ScriptableObject.ItemType)num)
                      .OrderBy(n => n.itemGrade)
                      .ToList();


        foreach (var items in gainitem)
        {
             item.Add(items);
        }
        

       for(int i=0; i < item.Count; i++)
        {
            itemSlots[i].Setting(item[i]);
        }
        
    }

    public void OnClick(int num)
    {
      for(int i=0; i < 6; i++)
        {
            if (i == num)
            {
                buttons[i].sprite = choiceButtonImage;
                buttonIcons[i].transform.localScale = new Vector3(1.2f, 1.2f, 1);
            }
            else
            {
                buttons[i].sprite = defaultButtonImgae;
                buttonIcons[i].transform.localScale = new Vector3(1f, 1f, 1);
            }
        }


        ReSetting(num);
    }

    public void OninfoPanel(Item item)
    {  
        iteminfo.Setting(item);
        iteminfo.gameObject.SetActive(true);
    }

}
