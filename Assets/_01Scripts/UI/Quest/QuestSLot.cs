using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuestSLot : MonoBehaviour
{
    [SerializeField]
    Image rewardIcon;
    [SerializeField]
    TextMeshProUGUI questNameText;
    [SerializeField]
    TextMeshProUGUI questCountText;
    [SerializeField]
    Slider questslider;
    [SerializeField]
    GameObject ativeQuestEffect;
    [SerializeField]
    GameObject completeQuest;
    [SerializeField]
    Button moveButton;
    [SerializeField]
    Button GetRewardButton;
    
    [SerializeField]
    QuestUI questUI;

    public void Setting(Sprite Icon, Quest quests)
    {
        if(quests != null)
        {
            rewardIcon.sprite = Icon;
            questNameText.text = quests.questdata.questName;
            questCountText.text = String.Format("({0}/{1})", quests.quest_Count, quests.questdata.numberOfGoal);
            questslider.maxValue = quests.questdata.numberOfGoal;
            questslider.value = quests.quest_Count;

            if (quests.questStat == Quest.QuestStat.RewardPaymented)
            {
                completeQuest.SetActive(true);
            }
            else if(quests.questStat == Quest.QuestStat.Complete)
            {
                completeQuest.SetActive(false);
                GetRewardButton.gameObject.SetActive(true);
                moveButton.gameObject.SetActive(false);
            }
            else
            {
                completeQuest.SetActive(false);
                GetRewardButton.gameObject.SetActive(false);
                moveButton.gameObject.SetActive(true);
            }
        }
        else
        {
            return;
        }
    }

    public void ChangeAtiveQuest(bool Ative)
    {
        ativeQuestEffect.SetActive(Ative);
    }



}
