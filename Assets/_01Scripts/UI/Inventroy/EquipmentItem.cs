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
        if (StatusManager.instance != null && statusManager == null) statusManager = StatusManager.instance;
        if (ItemManager.instance != null && itemManager == null) itemManager = ItemManager.instance;
    }

    private void OnEnable()
    {
       if(StatusManager.instance != null && statusManager ==null) statusManager = StatusManager.instance;
       if (ItemManager.instance != null && itemManager == null) itemManager = ItemManager.instance;

        itemManager?.ChangeEqument.AddListener(ResetEquipSLot);
        itemManager?.ChangeEqument.AddListener(StatusSetting);
        ResetEquipSLot();
        StatusSetting();
    }

    public void StatusSetting()
    {
        hP_Text.text = statusManager.LastStatus_Text(StatusManager.playerHP);
        aTK_Text.text = statusManager.LastStatus_Text(StatusManager.playerATkpow);
        dFN_Text.text = statusManager.LastStatus_Text(StatusManager.playerDefence);
        crtRate_Text.text = statusManager.LastStatus_Text(StatusManager.playerCrtRate);
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
        itemManager?.ChangeEqument.RemoveListener(ResetEquipSLot);
    }


}
