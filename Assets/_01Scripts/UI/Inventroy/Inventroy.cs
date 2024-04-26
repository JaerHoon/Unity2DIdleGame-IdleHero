using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class Inventroy : MonoBehaviour
{
    
    [SerializeField]
    List<ItemSlot> itemSlots = new List<ItemSlot>();
    [SerializeField]
    List<Image> buttonIcons = new List<Image>();
    [SerializeField]
    List<Image> buttons = new List<Image>();

    [SerializeField]
    Sprite defaultButtonImgae;
    [SerializeField]
    Sprite choiceButtonImage;


    private void OnEnable()
    {
        OnClick(0);
        //ReSetting();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReSetting(0);
        }
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

}