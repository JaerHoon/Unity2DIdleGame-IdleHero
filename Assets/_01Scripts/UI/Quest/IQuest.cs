using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestChecker
{
    Quest_ScriptableObject.QuestType questType { get; set; }

    public void UpdateQuestInfo();

}
