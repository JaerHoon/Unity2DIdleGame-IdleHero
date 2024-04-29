using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageDate", menuName = "Scriptable Object/Stage Data")]

public class StageData : ScriptableObject
{
    [SerializeField]
    private int StageNum;
    public int stageNum { get { return StageNum; } }

    [SerializeField]
    private int SlimeNum;
    public int slimeNum { get { return SlimeNum; } }

    [SerializeField]
    private int BatNum;
    public int batNum { get { return BatNum; } }

    [SerializeField]
    private int SpiderNum;
    public int spiderNum { get { return SpiderNum; } }

    [SerializeField]
    private int BabyDragonNum;
    public int babyDragonNum { get { return BabyDragonNum; } }

    [SerializeField]
    private int CurrentStageNum;
    public int currentStageNum { get { return CurrentStageNum; } set { CurrentStageNum = value; } }
}
