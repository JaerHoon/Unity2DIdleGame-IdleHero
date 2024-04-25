using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data")]

public class Quest_ScriptableObject : ScriptableObject
{
    public string questName;
    public int numberOfGoal;//목표달성횟수
    public int currentNumber;//현대 횟수
    public int rewordAmount;//보상의 양
}
