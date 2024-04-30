using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public Item_ScriptableObject itemData;
    int itemLV;
    int MaxLv=10;

    public int ItemLv
    {
        get
        {
            return itemLV;
        }
        set
        {
            if (value > MaxLv)
            {
                itemLV = MaxLv;
            }
            else
            {
                itemLV = value;
            }
        }
    }


    public Sprite backGround;
    public Sprite slot;
    public enum ItemGrade { Nomal, Rare, Unique, Epic, Legend }
    public ItemGrade itemGrade;
    

   

    public void Setting(Sprite background, Sprite slot)
    {
        this.backGround = background;
        this.slot = slot;
    }


    int CalItempow()
    {
        int num= (int)itemGrade+1;
        int itempow = itemData.itemPow * num;

        return itempow;
    }

    
    public int Cal_LevelupPow(int Lv)// 레벨 업당 올라가는 스탯의 양 계산
    {
        int pow;
        if(Lv <= 1)//레벨이 1일때 계산
        {
            pow = CalItempow() *  Lv;
        }
        else
        {
            pow = CalItempow() * itemData.LvUP_Rate * Lv;
            //레벨이 2이상일때 계산
        }
       
        return pow;
    }


}
