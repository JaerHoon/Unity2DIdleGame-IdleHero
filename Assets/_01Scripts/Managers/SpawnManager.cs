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




    //===============팩토리 선언=======================
    MonsterFactory babyDragonFactory; 
    
    void Start()
    {
        babyDragonFactory = new MonsterFactory(babyDragonPrefab,5);//몬스터 팩토리에 드래곤 인스턴스 생성
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RecyclableMonster babyDragon = babyDragonFactory.GetMonster();
            babyDragon.Activate(spawnPoint[0].position);
            
        }
    }
}
