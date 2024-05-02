using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class EquipmentItem : MonoBehaviour
{
    [SerializeField]
    List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
    [SerializeField]
    Iteminfo iteminfo;
    [SerializeField]
    TextMeshProUGUI hP_Text;
    [SerializeField]
    TextMeshProUGUI aTK_Text;
    [SerializeField]
    TextMeshProUGUI dFN_Text;
    [SerializeField]
    TextMeshProUGUI crtRate_Text;

    StatusManager statusManager;
    ItemManager itemManager;
    private void Start()
    {
        statusManager = StatusManager.instance;
        itemManager = ItemManager.instance;
    }

    private void OnEnable()
    {
        itemManager.ChangeEqument.AddListener(ResetEquipSLot);
        itemManager.ChangeEqument.AddListener(StatusSetting);
        ResetEquipSLot();
        StatusSetting();
    }

    public void StatusSetting()
    {
        hP_Text.text = String.Format("{0} (+{1})", 
            statusManager.GetStatus(StatusManager.playerHP),
            itemManager.GetItemPow(StatusManager.playerHP));
        aTK_Text.text = String.Format("{0} (+{1})",
            statusManager.GetStatus(StatusManager.playerATkpow),
            itemManager.GetItemPow(StatusManager.playerATkpow));
        dFN_Text.text = String.Format("{0} (+{1})",
            statusManager.GetStatus(StatusManager.playerDefence),
            itemManager.GetItemPow(StatusManager.playerDefence));
        crtRate_Text.text = String.Format("{0} (+{1})",
            statusManager.GetStatus(StatusManager.playerCrtRate),
            itemManager.GetItemPow(StatusManager.playerCrtRate));
    }

    public void ResetEquipSLot()
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            equipmentSlots[i].EquipItem(itemManager?.equipments[i]);
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
