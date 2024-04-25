using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;//스폰 매니저 싱글톤 생성

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Transform[] spawnPoint;
    //===============몬스터 프리팹 선언===================
    [SerializeField]
    BabyDragon babyDragonPrefab;
    [SerializeField]
    Slime slimePrefab;
    [SerializeField]
    Bat batPrefab;
    [SerializeField]
    Spider spiderPrefab;



    //===============팩토리 선언=======================
    MonsterFactory babyDragonFactory; 
    MonsterFactory slimeFactory; 
    MonsterFactory batFactory; 
    MonsterFactory spiderFactory; 
    
    void Start()
    {
        babyDragonFactory = new MonsterFactory(babyDragonPrefab,5);//몬스터 팩토리에 드래곤 인스턴스 생성
        slimeFactory = new MonsterFactory(slimePrefab, 5);//몬스터 팩토리에 슬라임 인스턴스 생성
        batFactory = new MonsterFactory(batPrefab, 5);//몬스터 팩토리에 뱃 인스턴스 생성
        spiderFactory = new MonsterFactory(spiderPrefab, 5);//몬스터 팩토리에 스파이더 인스턴스 생성
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RecyclableMonster babyDragon = babyDragonFactory.GetMonster();
            babyDragon.Activate(spawnPoint[0].position);
            
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            RecyclableMonster slime = slimeFactory.GetMonster();
            slime.Activate(spawnPoint[1].position); 

        }
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            RecyclableMonster bat = batFactory.GetMonster();
            bat.Activate(spawnPoint[2].position);

        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            RecyclableMonster spider = spiderFactory.GetMonster();
            spider.Activate(spawnPoint[3].position);

        }
    }
}
