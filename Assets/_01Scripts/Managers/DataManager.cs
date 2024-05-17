using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    string path;
    

    private void Awake()
    {
        if (instance == null) instance = this;
        path = Path.Combine(Application.dataPath, "Data", "database.json");
    }

    private void Start()
    {
        LoadData();
        Resource.instance.UpdateText();
    }

    void LoadData()
    {
        SaveData saveData = new SaveData();

        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if(saveData != null)
            {
                ItemManager.instance.LoadItem(saveData);
                StatusManager.instance.Hp_Lv = saveData.stat_Lv[0];
                StatusManager.instance.ATkpow_Lv = saveData.stat_Lv[1];
                StatusManager.instance.DFN_Lv = saveData.stat_Lv[2];
                StatusManager.instance.CrtRate_Lv = saveData.stat_Lv[3];
                Resource.instance.coinNum = saveData.gold;
                Resource.instance.jemNum = saveData.jewel;
                StageManager.instance.StageNum = saveData.stage;
                SkillManager.instance.ChangeSlot(saveData.skillSlot);
                QuestManager.instance.SettingQuest(saveData.ActiveQuestNum);
            }
        }
        else
        {
            print("새로생성");
            ItemManager.instance.CreatItem();
            StageManager.instance.StageNum = 0;
            StatusManager.instance.Hp_Lv = 1;
            StatusManager.instance.ATkpow_Lv = 1;
            StatusManager.instance.DFN_Lv = 1;
            StatusManager.instance.CrtRate_Lv = 1;
            Resource.instance.coinNum = 0;
            Resource.instance.jemNum = 0;
            QuestManager.instance.SettingQuest(0);
        }
    }

  
    void SaveData()
    {
        
        SaveData saveData = new SaveData();
        var (items, gainedItems, equippedItems) = InItemData();
        saveData.Init(items, gainedItems, equippedItems);
        saveData.stat_Lv[0] = StatusManager.instance.Hp_Lv;
        saveData.stat_Lv[1] = StatusManager.instance.ATkpow_Lv;
        saveData.stat_Lv[2] = StatusManager.instance.DFN_Lv;
        saveData.stat_Lv[3] = StatusManager.instance.CrtRate_Lv;
        saveData.gold = Resource.instance.coinNum;
        saveData.jewel = Resource.instance.jemNum;
        saveData.stage = StageManager.instance.StageNum;
        saveData.skillSlot = SkillManager.instance.OutSlotnum();
        saveData.ActiveQuestNum = QuestManager.instance.ativeQuest.questdata.quest_number;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
  
    }

    (List<ItemSaveData> items, List<ItemSaveData> gainedItems, ItemSaveData[] equipedItems) InItemData()
    {
        List<ItemSaveData> items = new List<ItemSaveData>();
        List<ItemSaveData> gainedItems = new List<ItemSaveData>();
        ItemSaveData[] equipedItems = new ItemSaveData[6];

        for (int i = 0; i < ItemManager.instance.items.Count; i++)
        {
            ItemSaveData item = new ItemSaveData(ItemManager.instance.items[i]);
            items.Add(item);
        }

        for(int i = 0; i < ItemManager.instance.gainedItems.Count; i++)
        {
            ItemSaveData gaineditem = new ItemSaveData(ItemManager.instance.gainedItems[i]);
            gainedItems.Add(gaineditem);
        }
        for(int i=0; i < ItemManager.instance.equipments.Length; i++)
        {
            ItemSaveData equipeditem = new ItemSaveData(ItemManager.instance.equipments[i] ?? null);
           
            equipedItems[i] = equipeditem;
        }


        return (items, gainedItems, equipedItems);

    }

    private void OnDisable()
    {
        SaveData();
    }
}
