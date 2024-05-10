using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class Player_ScriptableObject : ScriptableObject
{
    public int playerDamage; // 10으로 설정
    public int playerHP; // 100으로 설정
    public int playerDefence; // 10으로 설정
    public int playerCritical; // 5로 설정
    public int playerCriticalPower;
}
