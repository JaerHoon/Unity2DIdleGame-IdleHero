using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField]
    List<Quest_ScriptableObject> questsData = new List<Quest_ScriptableObject>();
    public List<Quest> quests = new List<Quest>();
    public Quest ativeQuest;

    public Sprite goldSprite;
    public Sprite jewelSprite;

    public UnityEvent OnChangeQuest;

    int cur_num;
    int goalNum;

    public SmallQuestUI smallQuestPanel;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

       //questpanel_BTn = questPanel.GetComponent<Button>();
    }

    private void Start()
    {
        for (int i = 0; i < questsData.Count; i++)
        {
            Quest quest = new Quest(questsData[i]);
            quests.Add(quest);
        }

       
    }


    public void SettingQuest(int num)
    {
       for(int i=0; i < num; i++)
        {
            quests[i].questStat = Quest.QuestStat.RewardPaymented;
        }
        OnAtiveQuest(num);
       
    }

    public void OnAtiveQuest(int num)
    {
        ativeQuest = quests[num];
        ativeQuest.ActiveQurst();
        smallQuestPanel.gameObject.SetActive(true);
        smallQuestPanel.Setting(ativeQuest);
        OnChangeQuest?.Invoke();
    }

    public void UpDateQuest(Quest_ScriptableObject.QuestType questType)
    {
        ativeQuest.UpdateQuest(questType);
        OnChangeQuest?.Invoke();
        smallQuestPanel.Setting(ativeQuest);

    }

    public void PaymentReward() // UI에서 받음
    {
        UISound.instance.PlayerSound(UISound.pressQuestReward);
        ativeQuest.questStat = Quest.QuestStat.RewardPaymented;

        Resource.instance.GetResource(0, ativeQuest.questdata.rewordAmount);
      

        if (ativeQuest.questdata.quest_number<= questsData.Count)
        {
            OnAtiveQuest(ativeQuest.questdata.quest_number + 1);
        }
        else
        {
            ativeQuest = null;

        }

        smallQuestPanel.Setting(ativeQuest);
        OnChangeQuest?.Invoke();


    }

   







}
