using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    [SerializeField]
    List<Item_ScriptableObject> item_ScriptableObject = new List<Item_ScriptableObject>();
    [SerializeField]
    List<Sprite> backgrounds = new List<Sprite>();
    [SerializeField]
    List<Sprite> slots = new List<Sprite>();

    public List<Item> items = new List<Item>();
    public List<Item> gainedItems = new List<Item>();

    
    private void Awake()
    {
        if(instance == null)
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
        List<Item_ScriptableObject> itemdatas = new List<Item_ScriptableObject>();
        itemdatas = item_ScriptableObject.Where(n => n.itemType == Item_ScriptableObject.ItemType.Weapon).ToList();
        ItemList(itemdatas);
        itemdatas.Clear();
        itemdatas = item_ScriptableObject.Where(n => n.itemType == Item_ScriptableObject.ItemType.Shield).ToList();
        ItemList(itemdatas);
        itemdatas.Clear();
        itemdatas = item_ScriptableObject.Where(n => n.itemType == Item_ScriptableObject.ItemType.Helmet).ToList();
        ItemList(itemdatas);
        itemdatas.Clear();
        itemdatas = item_ScriptableObject.Where(n => n.itemType == Item_ScriptableObject.ItemType.Armor).ToList();
        ItemList(itemdatas);
        itemdatas.Clear();
        itemdatas = item_ScriptableObject.Where(n => n.itemType == Item_ScriptableObject.ItemType.Shoes).ToList();
        ItemList(itemdatas);
        itemdatas.Clear();
        itemdatas = item_ScriptableObject.Where(n => n.itemType == Item_ScriptableObject.ItemType.Accessories).ToList();
        ItemList(itemdatas);
        itemdatas.Clear();

    } 
    
    void ItemList(List<Item_ScriptableObject> itemdatas)
    {
        for (int i = 0; i < itemdatas.Count; i++)
        {
            Item item = new Item();
            item.itemData = itemdatas[i];
            item.itemGrade = Item.ItemGrade.Nomal;
            item.Setting(backgrounds[0], slots[0]);
            item.ItemLv = 1;
            item.isGained = false;
            items.Add(item);
            for (int a = 0; a < 4; a++)
            {
                Item item2 = new Item();
                item2.itemData = itemdatas[i];
                switch (a)
                {
                    case 0: item2.itemGrade = Item.ItemGrade.Rare; break;
                    case 1: item2.itemGrade = Item.ItemGrade.Unique; break;
                    case 2: item2.itemGrade = Item.ItemGrade.Epic; break;
                    case 3: item2.itemGrade = Item.ItemGrade.Legend; break;
                }
                item2.Setting(backgrounds[a + 1], slots[a + 1]);
                item2.ItemLv = 1;
                item2.isGained = false;
                items.Add(item2);
            }
        }
    }

    public void GetItem(int itemnum)
    {
        gainedItems.Add(items[itemnum]);
        items.RemoveAt(itemnum);
    }


    // 디버그용 함수
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i < items.Count; i++)
            {
                print(items[i].itemData.itemname+ "/" + items[i].itemGrade);
                
            }
        }
    }*/
}
