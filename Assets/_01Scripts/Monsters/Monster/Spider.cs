using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : RecyclableMonster
{
    [SerializeField]
    MonsterData spiderData;
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

    bool istargetDetected = true;
    

   
    public float spiderAttackMovementSpeed = 5.0f;

    private void OnEnable()//활성화 시 초기화
    {
        monName = spiderData.monsterName;
        hp = spiderData.hp;
        damage = spiderData.damage;
        defense = spiderData.defense;
        moveSpeed = spiderData.moveSpeed;
        attackDistance = spiderData.attackDistance;
        attackSpeed = spiderData.attackSpeed;
        attackMotionSpeed = spiderData.attackMotionSpeed;
        Init();//부모에서 초기화
    }

    void Start()
    {
        gameObject.tag = "monster";
        anim = GetComponent<Animator>();
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

    //===============몬스터 상태에 따른 애니메이터 파라미터 값 변경==============
   
    public override void AttackState()
    {
        base.AttackState();
        if(istargetDetected)
        {
            istargetDetected = false;
            ReDetected();
            StartCoroutine(ReDetectedDelay(spiderData.attackMotionSpeed));
        }
       
        anim.SetInteger("STATE", 2);
        transform.position = Vector2.Lerp(transform.position, Ppos + dir* 1.5f, Time.deltaTime * spiderAttackMovementSpeed);
        //transform.position = Utility.EaseInQuint(transform.position, Ppos + dir * 1.5f, Time.deltaTime* spiderAttackMovementSpeed);//플레이어 방향으로 점프
         
    }
    Vector3 Ppos = Vector3.zero;//일시적 플레이어 위치 저장
    Vector3 dir = Vector3.zero;//일시적 플레이어 방향 저장
    void ReDetected()
    {
        Ppos = targetPosition.position;//위치 등록
        dir = (targetPosition.position - transform.position).normalized;//방향 등록
    }

    IEnumerator ReDetectedDelay(float time)//공격 전 딜레이 함수
    {
        yield return new WaitForSeconds(time);
        istargetDetected = true;
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
        anim.SetInteger("STATE", 4);
    }


    // Update is called once per frame
    void Update()
    {
        LookPlayer(targetPosition.position);
        MonsterState(targetPosition.position, spiderData.attackDistance, spiderData.attackSpeed, spiderData.attackMotionSpeed);
        UpdateState(targetPosition.position, spiderData.moveSpeed);
    }
}
