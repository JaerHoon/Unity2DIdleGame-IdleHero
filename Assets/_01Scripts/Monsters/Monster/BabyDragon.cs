using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BabyDragon : RecyclableMonster
{

    [SerializeField]
    MonsterData drangonData;
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

    

    [SerializeField]
    FireBall fireballPrefab;//파이어 볼 프리팹

    MonsterFactory fireBallFactory; //파이어 볼 펙토리

    private void OnEnable()//활성화 시 초기화
    {
        monName = drangonData.monsterName;
        hp = drangonData.hp;
        damage = drangonData.damage;
        defense = drangonData.defense;
        moveSpeed = drangonData.moveSpeed;
        attackDistance = drangonData.attackDistance;
        attackSpeed = drangonData.attackSpeed;
        attackMotionSpeed = drangonData.attackMotionSpeed;
        Init();//부모에서 초기화
    }

    void Start()
    {
        fireBallFactory = new MonsterFactory(fireballPrefab, 2);
        anim = GetComponent<Animator>();
    }

    public void OnMonDamaged(int PlayerDamage)//플레이어의 공격 이벤트를 받을 함수
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        if(hp <= 0)
        {
            Destroyed?.Invoke(this);//몬스터 죽음 이벤트
            isDead = true;

        }
        else
        {
            isDamaged = true;
            DelayDamaged(0.5f);
        }
    }

    

    //===============몬스터 상태에 따른 애니메이터 파라미터 값 변경==============
    public override void AttackState()
    {
        base.AttackState();
        anim.SetInteger("STATE", 2);
    }
    public override void IdleState()
    {
        base.IdleState();
        anim.SetInteger("STATE", 0);
    }
    public override void TraceState(Vector3 playerPos, float moveSpeed)
    {
        base.TraceState(playerPos, moveSpeed);
        anim.SetInteger("STATE", 0);
    }
    public override void DamagedState()
    {
        base.DamagedState();
        anim.SetInteger("STATE", 3);
    }
    public override void DieState()
    {
        base.DieState();
        anim.SetInteger("STATE", 4);
    }

    public void OnFireBallLaunched()
    {
        RecyclableMonster fireBall = fireBallFactory.GetMonster();
        fireBall.Activate(GetComponentsInChildren<Transform>()[2].transform.position);
        fireBall.FireBallDestroyed += OnFireBallDestoryed;
    }

    void OnFireBallDestoryed(RecyclableMonster usedFireBall)
    {
        usedFireBall.FireBallDestroyed -= OnFireBallDestoryed;
        fireBallFactory.MonsterRestore(usedFireBall);
    }

   
    //=====================================
    // Update is called once per frame
    void Update()
    {
        
        LookPlayer(targetPosition.position);
        MonsterState(targetPosition.position, drangonData.attackDistance ,drangonData.attackSpeed, drangonData.attackMotionSpeed);
        UpdateState(targetPosition.position, drangonData.moveSpeed);
    }
}
