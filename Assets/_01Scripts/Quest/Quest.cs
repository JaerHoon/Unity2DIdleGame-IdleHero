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
    }

    public void UpdateQuest(Quest_ScriptableObject.QuestType questType)
    {
        if(questType == questdata.questType)
        {
            quest_Count++;

            if (quest_Count >= questdata.numberOfGoal)
            {
                CompleteQuest();
            }
        }
        else
        {
            return;
        }
        

       
    }

    public void CompleteQuest()
    {
        questStat = QuestStat.Complete;
    }

}
