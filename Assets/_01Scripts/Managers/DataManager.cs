using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public DataManager instance;
    string path;
   

    private void Awake()
    {
        if (instance == null) instance = this;
        path = Path.Combine(Application.dataPath, "Data", "database.json");
    }

    private void Start()
    {
        LoadData();
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
            }
        }
        else
        {
            ItemManager.instance.CreatItem();
            StageManager.instance.StageNum = 1;
            StatusManager.instance.Hp_Lv = 1;
            StatusManager.instance.ATkpow_Lv = 1;
            StatusManager.instance.DFN_Lv = 1;
            StatusManager.instance.CrtRate_Lv = 1;
            Resource.instance.coinNum = 0;
            Resource.instance.jemNum = 0;
        }
    }

    void SaveData()
    {
        SaveData saveData = new SaveData();
        saveData.items = ItemManager.instance.items;
        saveData.gained_Items = ItemManager.instance.gainedItems;
        saveData.equiped_Item = ItemManager.instance.equipments;
        saveData.stat_Lv[0] = StatusManager.instance.Hp_Lv;
        saveData.stat_Lv[1] = StatusManager.instance.ATkpow_Lv;
        saveData.stat_Lv[2] = StatusManager.instance.DFN_Lv;
        saveData.stat_Lv[3] = StatusManager.instance.CrtRate_Lv;
        saveData.gold = Resource.instance.coinNum;
        saveData.jewel = Resource.instance.jemNum;
        saveData.stage = StageManager.instance.StageNum;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
  
    }

    private void OnDisable()
    {
        SaveData();
    }
}
