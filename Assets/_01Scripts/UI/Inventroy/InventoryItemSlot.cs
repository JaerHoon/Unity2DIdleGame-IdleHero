using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSlot : Slots
{
    [SerializeField]
    Inventroy inventroy;


    public void OnClick()
    {
        inventroy.OninfoPanel(item);
    }
}
