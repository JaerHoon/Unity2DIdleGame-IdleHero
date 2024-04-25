using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField]
    List<Quest_ScriptableObject> questsData = new List<Quest_ScriptableObject>();
    [SerializeField]
    GameObject questPanel;//퀘스트 UI 페널
    Button questpanel_BTn;//퀘스트 버튼 컴포넌트

    [SerializeField]
    Image rewordImgae; //보석 이미지
    [SerializeField]
    TextMeshProUGUI questName;//퀘스트 네임 컴포넌트
    [SerializeField]
    TextMeshProUGUI questCountText;//퀘스트 실행 횟수 컴포넌트

    int cur_num;
    int goalNum;
    public int ongoingQuest_num;
    
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

        questpanel_BTn = questPanel.GetComponent<Button>();
    }


    public void StartQuest(int num)
    {
        questpanel_BTn.enabled = false;
        ongoingQuest_num = num;
        cur_num = 0;
        goalNum = questsData[num].numberOfGoal;
        questPanel.SetActive(true);
        questName.text = questsData[num].name;
        questCountText.text = string.Format("({0}/{1}", cur_num, goalNum);
    }

    public void RunQuest()
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
    }



}
