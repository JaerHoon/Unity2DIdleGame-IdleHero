using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

    List<RecyclableMonster> monsterStore = new List<RecyclableMonster>();
    public Transform[] stageSpawnPoint;//스테이지 스폰 포인트

    public Action<RecyclableMonster> something;
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

        StageManager.instance.StartWave += OnStartSpawn;//웨이브 시작과 스폰시작 이벤트 연결
    }
    
    void OnMonsterDestroyed(RecyclableMonster usedMonster)
    {
        
        usedMonster.Destroyed -= OnMonsterDestroyed;
        usedMonster.ClearDestroyed -= OnMonsterDestroyed;
        int monsterIndex = monsterStore.IndexOf(usedMonster);//리스트의 인덱스 값 추출
        monsterStore.RemoveAt(monsterIndex);
        //monsterFactory.MonsterRestore(usedMonster);
        if (usedMonster.name == "slime(Clone)")
        {
            slimeFactory.MonsterRestore(usedMonster);
        }
        else if (usedMonster.name == "bat(Clone)") batFactory.MonsterRestore(usedMonster);
        else if (usedMonster.name == "spider(Clone)") spiderFactory.MonsterRestore(usedMonster);
        else if (usedMonster.name == "baby_dragon(Clone)") babyDragonFactory.MonsterRestore(usedMonster);
    }
    //void OnSlimeDestroyed(RecyclableMonster usedBabyDragon)
    //{
    //    usedBabyDragon.Destroyed -= OnSlimeDestroyed;
    //}
    //void OnBatDestroyed(RecyclableMonster usedBabyDragon)
    //{
    //    usedBabyDragon.Destroyed -= OnBatDestroyed;
    //}
    //void OnSpiderDestroyed(RecyclableMonster usedBabyDragon)
    //{
    //    usedBabyDragon.Destroyed -= OnSpiderDestroyed;
    //}

    public void OnStartSpawn(StageData stageData)
    {
        if (stageData.slimeNum > 0)
        {
            StartCoroutine(SpawnDelay(stageData.slimeNum,  slimeFactory));
        }
        if (stageData.batNum > 0)
        {
            StartCoroutine(SpawnDelay(stageData.batNum, batFactory));
        }
        if (stageData.spiderNum > 0)
        {
            StartCoroutine(SpawnDelay(stageData.spiderNum, spiderFactory));
        }
        if (stageData.babyDragonNum > 0)
        {
            StartCoroutine(SpawnDelay(stageData.babyDragonNum, babyDragonFactory));
        }




    }

    IEnumerator SpawnDelay(int MaxMonsterTypeNum, MonsterFactory monsterFactory)
    {
        int spawnNum = MaxMonsterTypeNum;
        int A = 0;
       
        while (spawnNum > 0)
        {
            int randomInt = UnityEngine.Random.Range(0, 8);
     
            yield return new WaitForSeconds(60/MaxMonsterTypeNum);//소환될 몬스터의 수 분에 30 초 마다 소환/60->1초에 한마리 20->3초애 한마리
        
            
            RecyclableMonster monster = monsterFactory.GetMonster();//몬스터 활성화
            spawnNum--;
            monster.Activate(stageSpawnPoint[randomInt].position);//스폰 위치 설정
            monster.Destroyed += OnMonsterDestroyed;
            monster.ClearDestroyed += OnMonsterDestroyed;
            monsterStore.Add(monster);//몬스터 제거하기 위해 담아두는 리스트


            A++;
            if(MaxMonsterTypeNum == A)
                Debug.Log($"{monster.name} {A} 마리 모두 소환됨");
            if (A > 1000)
                break;
        }

    }

    public void OnDestroyAllMonster()
    {
        //foreach (var monster in monsterStore)
        //{
        //    monster.OnStageClearMonDestroy();
        //}
        StopAllCoroutines();
        for (int i = monsterStore.Count -1; i >= 0; i--)
        {
            monsterStore[i].OnStageClearMonDestroy();
        }
    }

    public void OnDamagedAllMonster()
    {
        //foreach (var monster in monsterStore)
        //{
        //    monster.OnMonDamaged(3);
        //}
        for (int i = monsterStore.Count - 1; i >= 0; i--)
        {
            monsterStore[i].OnMonDamaged(3);
        }
    }

    void Update()
    {



        //=======================테스트용 소환======================================
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    RecyclableMonster babyDragon = babyDragonFactory.GetMonster();
        //    babyDragon.Activate(spawnPoint[0].position);
        //    babyDragon.Destroyed += OnBabyDragonDestroyed;
        //    //GameManager.instance.playerAttack.monAttack += babyDragon.MonDamaged;//몬스터 피격 이벤트 연결

        //}
        //if (Input.GetKeyDown(KeyCode.LeftAlt))
        //{
        //    RecyclableMonster slime = slimeFactory.GetMonster();
        //    slime.Activate(spawnPoint[1].position);
        //    slime.Destroyed += OnSlimeDestroyed;

        //}
        //if (Input.GetKeyDown(KeyCode.RightAlt))
        //{
        //    RecyclableMonster bat = batFactory.GetMonster();
        //    bat.Activate(spawnPoint[2].position);
        //    bat.Destroyed += OnBatDestroyed;

        //}
        //if (Input.GetKeyDown(KeyCode.RightControl))
        //{
        //    RecyclableMonster spider = spiderFactory.GetMonster();
        //    spider.Activate(spawnPoint[3].position);
        //    spider.Destroyed += OnSpiderDestroyed;

           

        //}

    }
}
