using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentItem : MonoBehaviour
{
    [SerializeField]
    List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
    [SerializeField]
    Iteminfo iteminfo;
  

    private void OnEnable()
    {
        ItemManager.instance?.ChangeEqument.AddListener(ResetEquipSLot);

        ResetEquipSLot();
    }

    public void ResetEquipSLot()
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            equipmentSlots[i].EquipItem(ItemManager.instance?.equipments[i]);
        }
    }

    public void OnItemInfo(Item item)
    {
        iteminfo.gameObject.SetActive(true);
        iteminfo.Setting(item);
    }

    private void OnDisable()
    {
        ItemManager.instance.ChangeEqument.RemoveListener(ResetEquipSLot);
    }


}
