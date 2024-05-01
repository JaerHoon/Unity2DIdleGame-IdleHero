using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        if (MyRenderer != null) SetAlpa();
        if (Mycollider2D != null) Mycollider2D.enabled = true;
        Init();//부모에서 초기화
    }

    void Start()
    {
        gameObject.tag = "monster";
        anim = GetComponent<Animator>();
        MyRenderer = gameObject.GetComponent<Renderer>();
        Mycollider2D = gameObject.GetComponent<CircleCollider2D>();
        DOTween.Init(false, true, LogBehaviour.Verbose).SetCapacity(200, 50);
    }

    public override void OnMonDamaged(int PlayerDamage)//플레이어의 공격 이벤트를 받을 함수
    {
        
        hp = MonDamaged(hp, defense, PlayerDamage);
        if (hp <= 0)
        {
            Mycollider2D.enabled = false;
            isDead = true;
            StartCoroutine(DelayDeath());
        }
        else
        {
            isDamaged = true;
            StartCoroutine(DelayDamaged(0.25f));
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
        if (!isShake)
        {
            transform.DOShakePosition(0.3f, 0.1f, 90, 180, false, false);
            isShake = true;
            Invoke("DelayShake", 0.31f);
        }
    }
    void DelayShake() { isShake = false; }

    public override void DieState()
    {
        base.DieState();
        anim.SetInteger("STATE", 4);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float f = 1;
        while (f > 0)
        {
            f -= 0.1f;
            Color ColorAlhpa = MyRenderer.material.color;
            ColorAlhpa.a = f;
            MyRenderer.material.color = ColorAlhpa;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void SetAlpa()
    {
        float f = 1;
        Color ColorAlhpa = MyRenderer.material.color;
        ColorAlhpa.a = f;
        MyRenderer.material.color = ColorAlhpa;
    }

    // Update is called once per frame
    void Update()
    {
        LookPlayer(targetPosition.position);
        MonsterState(targetPosition.position, spiderData.attackDistance, spiderData.attackSpeed, spiderData.attackMotionSpeed);
        UpdateState(targetPosition.position, spiderData.moveSpeed);

        
    }
}
