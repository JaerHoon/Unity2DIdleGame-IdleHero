using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestUI : MonoBehaviour
{
    [SerializeField]
    List<QuestSLot> questSlots = new List<QuestSLot>();
    [SerializeField]
    Sprite goldIcon;
    [SerializeField]
    Sprite jewel;

    QuestManager questManager;


    private void OnEnable()
    {
        if (questManager == null) questManager = QuestManager.instance ?? null;
        questManager?.OnChangeQuest.AddListener(Setting);
        Setting();
    }

    public void Setting()
    {
        if (questManager == null) return;
        if (questManager.quests.Count == 0) return;

       for(int i=0; i<questSlots.Count; i++)
        {
            Sprite sprite = 
                questManager?.quests[i]?.questdata.rewardType 
                == Quest_ScriptableObject.RewardType.Gold ? goldIcon : jewel;
            questSlots[i].Setting(sprite, questManager?.quests[i]);
            questSlots[i].ChangeAtiveQuest(false);
           

        }
        
        questSlots[questManager.ativeQuest.questdata.quest_number].ChangeAtiveQuest(true);

        for (int i = 0; i < questSlots.Count; i++)
        {
          
            Sprite sprite =
              questManager?.quests[i]?.questdata.rewardType
              == Quest_ScriptableObject.RewardType.Gold ? goldIcon : jewel;
            questSlots[i].Setting(sprite, questManager?.quests[i]);
          
        }




    }

    public void OnClick(int num)
    {
        if(num == 0)
        {
            switch (questManager.ativeQuest.questdata.quest_number)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    MainCanvas.instance.OnClick(0);
                    break;
                case 4:
                    MainCanvas.instance.OnClick(4);
                    break;
                case 5:
                    MainCanvas.instance.OnClick(1);
                    break;
                case 6:
                    MainCanvas.instance.Cancel();
                    break;

            }
        }
        else if (num == 1)
        {
           
            questManager?.PaymentReward();
        }
    }

    private void OnDisable()
    {
        questManager?.OnChangeQuest.RemoveAllListeners();
    }


}
