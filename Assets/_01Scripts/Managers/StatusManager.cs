using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusManager : MonoBehaviour
{
    StatusManager instance;

    public const int playerHP= 0;
    public const int playerATkpow = 1;
    public const int playerDefence = 2;
    public const int playerCrtRate = 3;

    int[] start_Staus = new int[4];
    int[] status_Lv = new int[4];

    public int Hp_Lv
    {
        get
        {
            return start_Staus[0];
        }
        set
        {
            start_Staus[0] = value;
        }
    }
    public int ATkpow_Lv
    {
        get
        {
            return start_Staus[1];
        }
        set
        {
            start_Staus[1] = value;
        }
    }

    public int DFN_Lv
    {
        get
        {
            return start_Staus[2];
        }
        set
        {
            start_Staus[2] = value;
        }
    }
    public int CrtRate_Lv 
    {
        get
        {
            return start_Staus[3];
        }
        set
        {
            start_Staus[3] = value;
        }
    }


    public int GetStatus(int stat)
    {
        int point = Cal_statPoint(stat) + ItemManager.instance.GetItemPow(stat);

        return point;
    }

    int Cal_statPoint(int stat)
    {
        int st = start_Staus[stat] * status_Lv[stat];
        return st;
    }

    private void Awake()
    {
        for (int i = 0; i < status_Lv.Length; i++) 
        {
            status_Lv[i] = 1;
        }
    }

}
