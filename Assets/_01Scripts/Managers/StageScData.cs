using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "StageScData", menuName = "Scriptable Object/StageSc Data", order = int.MaxValue)]
public class StageScData : ScriptableObject
{
    [SerializeField]
    private int CurrentStageNum;
    public int currentStageNum { get { return CurrentStageNum; } set { CurrentStageNum = value; } }

    [SerializeField]
    Stages[] stages;
    public Stages[] Stages { get { return stages; } }
}

[Serializable]
public struct Stages
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


}