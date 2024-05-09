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
        Init();
    }

    void Init()
    {
        for(int i=0; i < questsData.Count; i++)
        {
            Quest quest = new Quest(questsData[i]);
            quests.Add(quest);
        }

        OnAtiveQuest(0);
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
        //UPdate UI에도 카운트랑 상태도 전달
        OnChangeQuest?.Invoke();
        smallQuestPanel.Setting(ativeQuest);
    }

    public void PaymentReward() // UI에서 받음
    {
        ativeQuest.questStat = Quest.QuestStat.RewardPaymented;

        //리워드 지급 실행
        print("리워드 지급!");
       

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
    

    

    /*public void StartQuest(int num)
    {
        questpanel_BTn.enabled = false;
        ongoingQuest_num = num;
        cur_num = 0;
        goalNum = questsData[num].numberOfGoal;
        questPanel.SetActive(true);
        questName.text = questsData[num].name;
        questCountText.text = string.Format("({0}/{1}", cur_num, goalNum);
    }

    public void UpdateQuest()
    {
        cur_num++;
        questName.text = questsData[ongoingQuest_num].name;
        questCountText.text = string.Format("({0}/{1}", cur_num, goalNum);

        if(cur_num == goalNum)
        {
            EndQuest();
        }
    }

    void EndQuest()
    {
        questpanel_BTn.enabled = true;
        questCountText.text = "완료";
    }

    public void Onclick()
    {
        //리워드 주기 그리고 효과?
        if(ongoingQuest_num <= questsData.Count)
        {
            StartQuest(ongoingQuest_num + 1);
        }
        else
        {
            questPanel.SetActive(false);
        }
    }*/



}
