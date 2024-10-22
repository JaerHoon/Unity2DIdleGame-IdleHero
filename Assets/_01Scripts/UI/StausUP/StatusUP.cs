using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StatusUP : MonoBehaviour, IQuestChecker
{
    [SerializeField]
    PlayerDamaged player;

    [Header("플레이어창")]
    [SerializeField]
    TextMeshProUGUI playerNickNameText;
    [SerializeField]
    TextMeshProUGUI stageText;

    [Header("플레이어 최종스탯")]
    [SerializeField]
    TextMeshProUGUI lastHPText;
    [SerializeField]
    TextMeshProUGUI lastATKText;
    [SerializeField]
    TextMeshProUGUI lastDFNText;
    [SerializeField]
    TextMeshProUGUI lastCrtText;

    [Header("플레이어 스탯강화_HP")]
    [SerializeField]
    TextMeshProUGUI hP_LvText;
    [SerializeField]
    TextMeshProUGUI hP_BeforeValueText;
    [SerializeField]
    TextMeshProUGUI hP_AfterValueText;
    [SerializeField]
    TextMeshProUGUI hP_UPCostText;

    [Header("플레이어 스탯강화_ATK")]
    [SerializeField]
    TextMeshProUGUI atk_LvText;
    [SerializeField]
    TextMeshProUGUI atk_BeforeValueText;
    [SerializeField]
    TextMeshProUGUI atk_AfterValueText;
    [SerializeField]
    TextMeshProUGUI atk_UPCostText;

    [Header("플레이어 스탯강화_DFN")]
    [SerializeField]
    TextMeshProUGUI dfn_LvText;
    [SerializeField]
    TextMeshProUGUI dfn_BeforeValueText;
    [SerializeField]
    TextMeshProUGUI dfn_AfterValueText;
    [SerializeField]
    TextMeshProUGUI dfn_UPCostText;

    [Header("플레이어 스탯강화_Crt")]
    [SerializeField]
    TextMeshProUGUI crt_LvText;
    [SerializeField]
    TextMeshProUGUI crt_BeforeValueText;
    [SerializeField]
    TextMeshProUGUI crt_AfterValueText;
    [SerializeField]
    TextMeshProUGUI crt_UPCostText;

    public Quest_ScriptableObject.QuestType questType { get ; set ; }

    private void OnEnable()
    {
        Setting();
    }

    public void Setting()
    {
        ChangeStage();
        ChangeLvpoint();
        ChangeLastStat();
    }

    void ChangeStage()
    {
        if (StageManager.instance == null) return;
        stageText.text = String.Format("1-{0}", StageManager.instance.StageNum + 1);
    }

    void ChangeLastStat()
    {
        StatusManager statusManager = StatusManager.instance;
       
        lastHPText.text = statusManager?.LastStatus_Text(StatusManager.playerHP) ?? null ;
        lastATKText.text = statusManager?.LastStatus_Text(StatusManager.playerATkpow) ?? null;
        lastDFNText.text = statusManager?.LastStatus_Text(StatusManager.playerDefence)?? null;
        lastCrtText.text = statusManager?.LastStatus_Text(StatusManager.playerCrtRate) ?? null;
    }

    void ChangeLvpoint()
    {

        StatusManager statusManager = StatusManager.instance;
       

        hP_LvText.text = String.Format("Lv.{0}", statusManager?.Hp_Lv ?? 0);
        hP_BeforeValueText.text = statusManager?.LvStatus_text(StatusManager.playerHP, statusManager.Hp_Lv) ?? null;   
        hP_AfterValueText.text = statusManager?.LvStatus_text(StatusManager.playerHP, statusManager.Hp_Lv + 1) ?? null;
        hP_UPCostText.text = String.Format("{0}", statusManager?.Cal_StatUPCost(StatusManager.playerHP) ?? 0);

        atk_LvText.text = String.Format("Lv.{0}", statusManager?.ATkpow_Lv ?? 0);
        atk_BeforeValueText.text = statusManager?.LvStatus_text(StatusManager.playerATkpow, statusManager.ATkpow_Lv) ?? null;
        atk_AfterValueText.text = statusManager?.LvStatus_text(StatusManager.playerATkpow, statusManager.ATkpow_Lv + 1) ?? null;
        atk_UPCostText.text = String.Format("{0}", statusManager?.Cal_StatUPCost(StatusManager.playerATkpow) ?? 0);

        dfn_LvText.text = String.Format("Lv.{0}", statusManager?.DFN_Lv ?? 0);
        dfn_BeforeValueText.text = statusManager?.LvStatus_text(StatusManager.playerDefence, statusManager.DFN_Lv) ?? null;
        dfn_AfterValueText.text = statusManager?.LvStatus_text(StatusManager.playerDefence, statusManager.DFN_Lv + 1) ?? null;
        dfn_UPCostText.text = String.Format("{0}", statusManager?.Cal_StatUPCost(StatusManager.playerDefence) ?? 0);

        crt_LvText.text = String.Format("Lv.{0}", statusManager?.CrtRate_Lv ?? 0);
        crt_BeforeValueText.text = statusManager?.LvStatus_text(StatusManager.playerCrtRate, statusManager.CrtRate_Lv) ?? null;
        crt_AfterValueText.text = statusManager?.LvStatus_text(StatusManager.playerCrtRate, statusManager.CrtRate_Lv + 1) ?? null;
        crt_UPCostText.text = String.Format("{0}", statusManager?.Cal_StatUPCost(StatusManager.playerCrtRate) ?? 0);

    }


    public void OnClick(int num)
    {
        StatusManager statusManager = StatusManager.instance;
        if (statusManager == null) return;
        if (Resource.instance == null) return;
        
        if(Resource.instance.coinNum >= statusManager.Cal_StatUPCost(num))
        {
            if(Resource.instance.coinNum < 0)
            {
                print(Resource.instance.coinNum);
                print(statusManager.Cal_StatUPCost(num));
            }
            Resource.instance.SubResource(statusManager.Cal_StatUPCost(num), 0);
            switch (num)
            {
                case 0:
                    statusManager.Hp_Lv++;
                    player.HpCaculate(5);
                    questType = Quest_ScriptableObject.QuestType.HP_Enhancement;
                    break;
                case 1:
                    statusManager.ATkpow_Lv++;
                    questType = Quest_ScriptableObject.QuestType.ATK_Enhancement;
                    break;
                case 2:
                    statusManager.DFN_Lv++;
                    questType = Quest_ScriptableObject.QuestType.DFN_Enhancement;
                    break;
                case 3:
                    statusManager.CrtRate_Lv++;
                    questType = Quest_ScriptableObject.QuestType.CrtRate_Enhancement;
                    break;
            }

          

            UpdateQuestInfo();
            Setting();
        }
        else
        {
            return;
        }


    }

    public void UpdateQuestInfo()
    {
        QuestManager.instance.UpDateQuest(questType);
    }
}
