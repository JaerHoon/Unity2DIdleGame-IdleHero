using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

    public List<Item> items = new List<Item>(); // 생성된 아이템 객체를 담을 리스트
    public List<Item> gainedItems = new List<Item>(); // 플레이어가 얻은 아이템 객체를 담을 리스트

    
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
        ListItem();
    }

    void ListItem() // 아이템데이터를 탑입 중심으로 다시 정렬해서 리스트화 하는 함수
    {
        List<Item_ScriptableObject> itemdatas = new List<Item_ScriptableObject>(); //아이템을 받아서 새롭게 정렬하기 위해 생성한 함수

        int eVaues = Enum.GetValues(typeof(Item_ScriptableObject.ItemType)).Length;//아이템 타입의 개수를 받을 변수
        
        for (int i=0; i<eVaues; i++)
        {
            itemdatas = item_ScriptableObject.Where(n => n.itemType == (Item_ScriptableObject.ItemType)i).ToList();
            CreatItem(itemdatas);
            itemdatas.Clear();
        }
       
    } 
    
    void CreatItem(List<Item_ScriptableObject> itemdatas)// 아이템 객체 생성 및 각 아이템객체에 기본 설정을 해주는 함수 
    {
        for (int i = 0; i < itemdatas.Count; i++)
        {
            Item item = new Item(); //아이템 객체 생성
            item.itemData = itemdatas[i]; // CreatItem() 함수에서 받아온 itemdatas 데이터를 넣습니다. 
            item.itemGrade = Item.ItemGrade.Nomal; // 아이템 등급 입력
            item.Setting(backgrounds[0], slots[0]); // 아이템 슬롯 백그라운드와 스롯의 스프라이트 입력
            item.ItemLv = 1; // 아이템 레벨 초기화
            items.Add(item);// 생성된 아이템 객체를 items리스트에 삽입
            for (int a = 0; a < 4; a++)// 위에서 생성된 item의 4가지 등급을 더 생성해주기 위한 반복문
            {
                Item item2 = new Item();
                item2.itemData = itemdatas[i];
                item2.itemGrade = (Item.ItemGrade)a + 1;
                item2.Setting(backgrounds[a + 1], slots[a + 1]);
                item2.ItemLv = 1;
                items.Add(item2);
            }
        } 
    }

    public void GetItem(int itemnum)//플레이어가 아이템을 얻었을때 동작하는 함수
    {
        gainedItems.Add(items[itemnum]);// 아이템을 gainedItems리스트에 삽입
        items.RemoveAt(itemnum); // items 리스트에서 제거
    }

   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < items.Count; i++)
            {
                print(i +"/"+items[i].itemGrade);
            }
            
        }
    }*/


}
