using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSummon : MonoBehaviour
{
    [SerializeField]
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    public void OnClick1()
    {
        SummonsItem(1);
    }

    public void OnClick10()
    {
        SummonsItem(10);
    }

    void SummonsItem(int num)
    {
        int[] nums = Utility.RandomCreate(num, 0, ItemManager.instance.items.Count);

        for (int i = 0; i < nums.Length; i++)
        {
            ItemManager.instance.GetItem(nums[i]);
            itemSlots[i].Setting(ItemManager.instance.items[nums[i]]);
            itemSlots[i].gameObject.SetActive(true);
        }
    }
}
