using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentItem : MonoBehaviour
{
    [SerializeField]
    List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();

  

    private void OnEnable()
    {
        ItemManager.instance?.ChangeEqument.AddListener(EquipItem);

        for(int i=0; i < equipmentSlots.Count; i++)
        {
            equipmentSlots[i].EquipItem(ItemManager.instance?.equipments[i]);
        }
    }

    public void EquipItem(Item item)
    {
        equipmentSlots[(int)item.itemData.itemType].EquipItem(item);
    }

    private void OnDisable()
    {
        ItemManager.instance.ChangeEqument.RemoveListener(EquipItem);
    }


}
