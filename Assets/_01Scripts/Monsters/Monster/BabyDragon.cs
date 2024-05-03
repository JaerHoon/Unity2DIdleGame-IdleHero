using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    const float cCtime = 1.8f;//군중제어 저항 시간
    

    [SerializeField]
    FireBall fireballPrefab;//파이어 볼 프리팹
    [SerializeField]
    GameObject DamageTextPreFab;//데미지 텍스트 프리팹

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
        coinValue = drangonData.coinValue;
        if (MyRenderer != null) SetAlpa();
        if (Mycollider2D != null) Mycollider2D.enabled = true;
        transform.GetChild(0).localPosition = new Vector2(0, -0.29f);
        Init();//부모에서 초기화
    }

    void Start()
    {
        fireBallFactory = new MonsterFactory(fireballPrefab, 2);
        anim = GetComponent<Animator>();
        MyRenderer = gameObject.GetComponent<Renderer>();
        Mycollider2D = gameObject.GetComponent<CircleCollider2D>();
        DOTween.Init(false, true, LogBehaviour.Verbose).SetCapacity(200, 50);
    }

    public override void OnMonDamaged(int PlayerDamage)//플레이어의 공격 이벤트를 받을 함수
    {
        hp = MonDamaged(hp, defense, PlayerDamage);//피격 시 몬스터 Hp 계산
        GameObject damageText = Instantiate(DamageTextPreFab,transform.position + Vector3.up * 0.5f,Quaternion.identity);//피격 시 데미지 텍스트 생성
        damageText.GetComponent<DamageText>().damage = MonDamagedTextCal(drangonData.defense,PlayerDamage);//데미지 값을 계산해서 넘겨줌
        if (hp <= 0)
        {
            Mycollider2D.enabled = false;
            isDead = true;
            MonDeath?.Invoke(this);//코인생성, 죽었을때 즉각 이벤트
            StartCoroutine(DelayDeath());
        }
        else
        {
            if (!isCCTime)//군중제어 저항 시간이 지나면
            {
                isDamaged = true;
                isCCTime = true;
                StartCoroutine(DelayDamaged(0.25f));
                Invoke("SetisCCtimeF", cCtime);
            }
        }
    }

    void SetisCCtimeF()
    {
        isCCTime = false;
    }

    IEnumerator DelayDeath()//회수 전 죽는 애니메이션 재생 시간 확보
    {
        yield return new WaitForSeconds(2.5f);
        Destroyed?.Invoke(this);//몬스터 죽음 이벤트
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
        if (!isShake)
        {
            transform.DOShakePosition(0.3f,0.1f,90,180,false,false);
            isShake = true;
            Invoke("DelayShake", 0.31f);
        }
    }
    void DelayShake() { isShake = false; }

    public override void DieState()
    {
        base.DieState();
        if (!isOnceDieState)
        {
            anim.SetInteger("STATE", 5);
            Fall();
        }
    }

    void Fall()
    {
        isOnceDieState = true;
        transform.DOMoveY(transform.position.y - 0.8f, 1f).SetEase(Ease.OutBounce);
        transform.GetChild(0).DOMoveY(transform.position.y - 1.5f, 1).SetEase(Ease.OutBounce);
        StartCoroutine(DeathAnim());
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("STATE", 4);
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.5f);
        float f = 1;
        while (f > 0)
        {
            f -= 0.05f;
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

    public void OnFireBallLaunched()
    {
        RecyclableMonster fireBall = fireBallFactory.GetMonster();
        fireBall.Activate(GetComponentsInChildren<Transform>()[2].transform.position);
        fireBall.FireBallDestroyed += OnFireBallDestoryed;
        FireBall fb = fireBall as FireBall;
        fb.SetDamage(drangonData.damage);
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
