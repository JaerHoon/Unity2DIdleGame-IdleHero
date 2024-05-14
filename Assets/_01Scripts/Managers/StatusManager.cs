using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;

    public const int playerHP= 0;
    public const int playerATkpow = 1;
    public const int playerDefence = 2;
    public const int playerCrtRate = 3;

    int[] start_Staus = new int[4] {100, 10, 10, 50};
    int[] status_Lv = new int[4] { 1, 1, 1, 1 };

    public int Hp_Lv
    {
        get
        {
            return status_Lv[0];
        }
        set
        {
            status_Lv[0] = value;
        }
    }
    public int ATkpow_Lv
    {
        get
        {
            return status_Lv[1];
        }
        set
        {
            status_Lv[1] = value;
        }
    }

    public int DFN_Lv
    {
        get
        {
            return status_Lv[2];
        }
        set
        {
            status_Lv[2] = value;
        }
    }
    public int CrtRate_Lv 
    {
        get
        {
            return status_Lv[3];
        }
        set
        {
            status_Lv[3] = value;
        }
    }
    [SerializeField]
    PlayerDamaged player;


    private void Awake()
    {
        if (instance == null) instance = this; 
    }

    private void Start()
    {
      
    }

    public int GetStatus(int stat) //각 스텟을 계산해서 반환하는 함수
    {
        ItemManager itemManager = ItemManager.instance;
        if (itemManager == null) return 0;

        int point = Cal_statPoint(stat, status_Lv[stat]) + itemManager.GetItemPow(stat);
        // 기본스텟과 스텟레벨을 곱한 값 + 아이템 증가량

        return point;
    }

    public int Cal_statPoint(int stat, int LvValue)// 기본 스텟과 스텟 레벨을 곱해서 스탯을 계산해주는 함수
    {
        int st = 0;

        switch (stat)
        {
            case 0: st = start_Staus[stat] + (5 * (LvValue-1));
               
                break; // 레벨당 생명력 5
            case 1: st = start_Staus[stat] + (1 * (LvValue-1)); break; // 레벨당 공격력 1
            case 2: st = start_Staus[stat] + (1 * (LvValue-1)); break; // 레벨당 방어력 1
            case 3: st = start_Staus[stat] + (1 * (LvValue-1)); break; // 레벨당 확률 0.1;
        }

        return st;
    }

    public int Cal_StatUPCost(int stat) // 레벨업때 골드 비용 계산해주는 함수
    {
        int cost = 10 + (3 * (status_Lv[stat]-1));

        return cost;
    }

    public string LastStatus_Text(int stat) // 최종스탯 계산해서 스트링으로 반환해주는 함수
    {
        ItemManager itemManager = ItemManager.instance;
        if (itemManager == null) return null;
        string te;
        if(stat == 3)
        {
            te = String.Format("{0} (+{1})%",
            (float)GetStatus(stat) / 10,
            (float)itemManager.GetItemPow(stat) / 10);
        }
        else
        {
            te = String.Format("{0} (+{1})",
          GetStatus(stat),
          itemManager.GetItemPow(stat));
        }

        return te;
    }

    public string LvStatus_text(int stat, int Lv)// 아이템 빼고 레벨업당 스탯만 계산해서 스트링으로 변환해주는 함수
    {
        string te;

        if(stat == 3)
        {
            te = String.Format("{0}%",(float)Cal_statPoint(stat, Lv)/10);
        }
        else
        {
            te = String.Format("{0}", Cal_statPoint(stat, Lv));
        }

        return te;
    }

    

}
