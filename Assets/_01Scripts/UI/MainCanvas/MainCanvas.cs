using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas instance;

    [Header("패널")]
    [SerializeField]
    GameObject[] panels = new GameObject[5];

    [Header("버튼")]
    [SerializeField]
    Button[] manuButtons = new Button[5];
    [SerializeField]
    Button cancelButton;
    [SerializeField]
    Button cancelButton2;

    [SerializeField]
    ItemSummon itemSummon;

    private void Awake()
    {
        instance = this;
    }


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
        cancelButton.gameObject.SetActive(true);
       
        if(Btn_num == 3)
        {
            cancelButton2.gameObject.SetActive(false);
        }
        else
        {
            cancelButton2.gameObject.SetActive(true);
        }
    


        if (itemSummon.IsSummoning == false)
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

        cancelButton.gameObject.SetActive(false);
        cancelButton2.gameObject.SetActive(false);

       
    }
    


}
