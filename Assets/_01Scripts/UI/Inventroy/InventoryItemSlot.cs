using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSlot : Slots
{
    [SerializeField]
    Inventroy inventroy;


    public void OnClick()
    {
        UISound.instance.PlayerSound(UISound.pressButton);
        inventroy.OninfoPanel(item);
    }
}
