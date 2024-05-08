using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data")]

public class Quest_ScriptableObject : ScriptableObject
{
    public int quest_number;
    public string questName;
    public string questExplain;
    public int numberOfGoal;//목표달성횟수
    public int rewordAmount;//보상의 양

    public enum RewardType { Gold, jewel }
    public RewardType rewardType;

    public enum QuestType { HP_Enhancement, ATK_Enhancement, DFN_Enhancement, CrtRate_Enhancement, Summon, Equipment_Enhancement, StageClear }
    public QuestType questType;
    }
