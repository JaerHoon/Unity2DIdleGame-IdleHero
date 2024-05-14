using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quest
{
    public enum QuestStat { BeforeAtive, Ative, Complete, RewardPaymented }
    public QuestStat questStat;

    public Quest_ScriptableObject questdata;

    int QuestCount;

    public int quest_Count
    {
        get { return QuestCount; }
        
        set 
        { if(value >= questdata.numberOfGoal)
            {
                QuestCount = questdata.numberOfGoal;
            }
            else
            {
                QuestCount = value;
            }
        }
    }
    

    public Quest(Quest_ScriptableObject quest)
    {
        questdata = quest;
        questStat = QuestStat.BeforeAtive;
        QuestCount = 0;
    }

     public void ActiveQurst()
    {
        questStat = QuestStat.Ative;


        switch ((int)questdata.questType)
        {
            case 0:
                quest_Count = StatusManager.instance.Hp_Lv - 1;
                break;
            case 1:
                quest_Count = StatusManager.instance.ATkpow_Lv-1;
                break;
            case 2:
                quest_Count = StatusManager.instance.DFN_Lv-1;
                break;
            case 3:
                quest_Count = StatusManager.instance.CrtRate_Lv-1;
                break;
        }

        if (quest_Count >= questdata.numberOfGoal)
        {
            CompleteQuest();
        }


    }

    public void UpdateQuest(Quest_ScriptableObject.QuestType questType)
    {
        if(questdata.questType == questType)
        {
            quest_Count++;

            if (quest_Count >= questdata.numberOfGoal)
            {
                CompleteQuest();
            }

            else
            {
                return;
            }
        }
            
        
    }

    public void CompleteQuest()
    {
        questStat = QuestStat.Complete;
    }

}
