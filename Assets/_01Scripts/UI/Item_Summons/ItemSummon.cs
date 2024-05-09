using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSummon : MonoBehaviour, IQuestChecker
{
    [SerializeField]
    GameObject boxPanel;
    [SerializeField]
    GameObject summonsPanel;
    [SerializeField]
    GameObject buttonPanel;
    [SerializeField]
    GameObject summonsEffect;
    [SerializeField]
    GridLayoutGroup gridLayout;

    [SerializeField]
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    public bool IsSummoning;

    public Quest_ScriptableObject.QuestType questType { get ; set ; }

    private void OnEnable()
    {
        boxPanel.SetActive(true);
        summonsPanel.SetActive(false);
        buttonPanel.SetActive(true);
        summonsEffect.SetActive(false);

        foreach (ItemSlot it in itemSlots)
        {
            it.gameObject.SetActive(false);
        }
    }

    void ReSetting()
    {
        
        gridLayout.enabled = true;
        itemSlots[0].transform.localScale = new Vector3(1, 1, 1);

        foreach (ItemSlot it in itemSlots)
        {
            it.gameObject.SetActive(false);
        }
    }

    public void OnClick(int num)
    {
        if (!IsSummoning)
        {
            IsSummoning = true;
            ReSetting();
            StartCoroutine(SummonsItem(num));
        }
        else
        {
            return;
        }
        
    }

  
    IEnumerator SummonsItem(int num)
    {
        summonsPanel.SetActive(false);
        boxPanel.SetActive(true);
        summonsEffect.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        summonsEffect.SetActive(false);
        boxPanel.SetActive(false);
        summonsPanel.SetActive(true);
        boxPanel.SetActive(false);


        for (int i = 0; i < num; i++)
        {
            int nums = Random.Range(0, ItemManager.instance.items.Count);

            itemSlots[i].Setting(ItemManager.instance.items[nums]);
            ItemManager.instance.GetItem(nums);

            if (num ==1)
            {
                gridLayout.enabled = false;
                itemSlots[i].transform.localPosition = new Vector3(0, 0, 0);
                itemSlots[i].transform.localScale = new Vector3(1.2f, 1.2f, 1);
            }

            itemSlots[i].gameObject.SetActive(true);
            UpdateQuestInfo();
            yield return new WaitForSeconds(0.3f);
        }

        StopAllCoroutines();
        IsSummoning = false;
    }

    public void UpdateQuestInfo()
    {
        questType = Quest_ScriptableObject.QuestType.Summon;
        QuestManager.instance.UpDateQuest(questType);
    }
}
