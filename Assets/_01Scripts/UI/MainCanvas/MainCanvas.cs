using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [Header("패널")]
    [SerializeField]
    GameObject[] panels = new GameObject[4];

    [Header("버튼")]
    [SerializeField]
    Button[] manuButtons = new Button[4];
    [SerializeField]
    Button cancelButton;

    [SerializeField]
    ItemSummon itemSummon;

    private void OnEnable()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(true);
        }

        Cancel();
    }

    public void OnClick(int Btn_num)
    {
        if(itemSummon.IsSummoning == false)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (i == Btn_num)
                {
                    panels[i].SetActive(true);
                }
                else
                {
                    panels[i].SetActive(false);
                }
            }
        }
        else
        {
            return;
        }

       
     }

    public void Cancel()
    {
        if(itemSummon.IsSummoning == false)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(false);
            }
        }
        else
        {
            return;
        }

       
    }
    


}
