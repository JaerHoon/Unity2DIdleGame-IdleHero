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
   // [SerializeField]
    //TextMeshProUGUI itemName;

    public void Setting(Item item)
    {
        itemIcon.sprite = item.itemData.icon;
        backGround.sprite = item.backGround;
        slot.sprite = item.slot;
    }
}
