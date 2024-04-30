using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : RecyclableMonster
{

    [SerializeField]
    MonsterData slimeData;
    //==================선언=========================
    [SerializeField]
    string monName;
    [SerializeField]
    int hp;
    [SerializeField]
    int damage;
    [SerializeField]
    int defense;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float attackDistance;
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    float attackMotionSpeed;

    private void OnEnable()//활성화 시 초기화
    {
        monName = slimeData.monsterName;
        hp = slimeData.hp;
        damage = slimeData.damage;
        defense = slimeData.defense;
        moveSpeed = slimeData.moveSpeed;
        attackDistance = slimeData.attackDistance;
        attackSpeed = slimeData.attackSpeed;
        attackMotionSpeed = slimeData.attackMotionSpeed;
        Init();//부모에서 초기화
    }

    void Start()
    {
        gameObject.tag = "monster";
    }

    public void OnMonDamaged(int PlayerDamage)//플레이어의 공격 이벤트를 받을 함수
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        if (hp <= 0)
        {
            Destroyed?.Invoke(this);//몬스터 죽음 이벤트
            isDead = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        LookPlayer(targetPosition.position);
        MonsterState(targetPosition.position, slimeData.attackDistance, slimeData.attackSpeed, slimeData.attackMotionSpeed);
        UpdateState(targetPosition.position, slimeData.moveSpeed);
    }
}
