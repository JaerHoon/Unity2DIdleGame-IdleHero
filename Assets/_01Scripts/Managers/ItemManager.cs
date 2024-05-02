using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    [SerializeField]
    List<Item_ScriptableObject> item_ScriptableObject = new List<Item_ScriptableObject>(); // item데이터 리스트
    [SerializeField]
    List<Sprite> backgrounds = new List<Sprite>();// 등급별 아이템 슬롯 백그라운드 스프라이트 리스트
    [SerializeField]
    List<Sprite> slots = new List<Sprite>(); // 등급별 아이템 슬롯의 슬롯 스프라이트 리스트

   
    public Sprite defaultBackGround; // 빈슬롯 백그라운드
    public Sprite defaultSlot;

    public List<Sprite> defaultItemIcon = new List<Sprite>();

    public List<Item> items = new List<Item>(); // 생성된 아이템 객체를 담을 리스트
    public List<Item> gainedItems = new List<Item>(); // 플레이어가 얻은 아이템 객체를 담을 리스트

    public Item[] equipments = new Item[6];

    public UnityEvent ChangeEqument;
    
    private void Awake()
    {
        if(instance == null)//싱글톤
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        CreatItem();
    }

    void CreatItem()
    {
        int eVaues = Enum.GetValues(typeof(Item.ItemGrade)).Length;

        List<Item> itemdatas = new List<Item>();

        for (int i=0; i < item_ScriptableObject.Count; i++)
        {
           
            for (int a = 0; a < eVaues; a++)
            {
                Item item = new Item()
                {
                    itemData = item_ScriptableObject[i],
                    itemGrade = (Item.ItemGrade)a,
                    backGround = backgrounds[a],
                    slot = slots[a],
                    ItemLv = 1

                };

                itemdatas.Add(item);
            }
        }

        items = itemdatas.OrderBy(data => data.itemData.itemType)
                .ThenBy(data => data.itemData.itemPow)
                .ThenBy(data =>data.itemGrade)
                .ToList();
    
    }

    
    public void GetItem(int itemnum)//플레이어가 아이템을 얻었을때 동작하는 함수
    {
        gainedItems.Add(items[itemnum]);// 아이템을 gainedItems리스트에 삽입
        items.RemoveAt(itemnum); // items 리스트에서 제거
    }

    public void OnEquipItem(Item item)
    {
        equipments[(int)item.itemData.itemType] = item;
        ChangeEqument?.Invoke();
    }

    public int GetItemPow(int stat)
    {
        int status = 0;

        switch (stat)
        {
            case 0://HP 헬멧, 신발
                status = equipments[2].Cal_LevelupPow(equipments[2].ItemLv)
                    + equipments[4].Cal_LevelupPow(equipments[4].ItemLv);
                break;
            case 1:
                status = equipments[0].Cal_LevelupPow(equipments[0].ItemLv);
                break;
            case 2:
                status = equipments[1].Cal_LevelupPow(equipments[1].ItemLv)
                        + equipments[3].Cal_LevelupPow(equipments[3].ItemLv);
                break;

            case 3:
                status = equipments[5].Cal_LevelupPow(equipments[5].ItemLv);
                break;
        }

        return status;
        
    }
   
}
