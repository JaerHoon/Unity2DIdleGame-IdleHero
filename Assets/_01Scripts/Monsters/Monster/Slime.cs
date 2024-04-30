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
        anim = GetComponent<Animator>();
    }

    public override void OnMonDamaged(int PlayerDamage)//플레이어의 공격 이벤트를 받을 함수
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        if (hp <= 0)
        {
            
            isDead = true;
            StartCoroutine(DelayDeath());
        }
        else
        {
            isDamaged = true;
            StartCoroutine(DelayDamaged(0.7f));
        }
    }

    IEnumerator DelayDeath()//회수 전 죽는 애니메이션 재생 시간 확보
    {
        yield return new WaitForSeconds(1f);
        Destroyed?.Invoke(this);//몬스터 죽음 이벤트
    }

    //===============몬스터 상태에 따른 애니메이터 파라미터 값 변경==============
    public override void AttackState()
    {
        base.AttackState();
        anim.SetInteger("STATE", 0);
    }
    public override void IdleState()
    {
        base.IdleState();
        anim.SetInteger("STATE", 0);
    }
    public override void TraceState(Vector3 playerPos, float moveSpeed)
    {
        base.TraceState(playerPos, moveSpeed);
        anim.SetInteger("STATE", 1);
    }
    public override void DamagedState()
    {
        base.DamagedState();
        anim.SetInteger("STATE", 3);
    }
    public override void DieState()
    {
        base.DieState();
        if (!isOnceDieState)
        {
            
            anim.SetInteger("STATE", 4);
            isOnceDieState = true;
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
