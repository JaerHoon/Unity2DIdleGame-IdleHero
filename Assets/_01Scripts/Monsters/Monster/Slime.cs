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

    const float cCtime = 0.8f;//군중제어 저항 시간

    [SerializeField]
    GameObject DamageTextPreFab;//데미지 텍스트 프리팹

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
        coinValue = slimeData.coinValue;
        if (MyRenderer != null) SetAlpa();
        if (Mycollider2D != null) Mycollider2D.enabled = true;
        Init();//부모에서 초기화
        StartCoroutine(AttackPlayer());//공격 함수 업데이트 0.2초 마다
    }

    void Start()
    {
        gameObject.tag = "monster";
        anim = GetComponent<Animator>();
        MyRenderer = gameObject.GetComponent<Renderer>();
        Mycollider2D = gameObject.GetComponent<CircleCollider2D>();
        collRange = 0.35f;
    }

    public override void OnMonDamaged(int PlayerDamage)//플레이어의 공격 이벤트를 받을 함수
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        GameObject damageText = Instantiate(DamageTextPreFab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        damageText.GetComponent<DamageText>().damage = MonDamagedTextCal(slimeData.defense, PlayerDamage);
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

    IEnumerator AttackPlayer()//플레이어 공격 이벤트 발생 함수
    {
        while (!isDead)
        {
            Collider2D recognitionPlayer = Physics2D.OverlapCircle(transform.position + Vector3.up * -0.05f, collRange, layermask, -100.0f, 100.0f);
            if (recognitionPlayer != null)
            {
                PlayerAttack?.Invoke(damage);//몬스터->플레이어 공격 이벤트
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDrawGizmos()//공갹 충돌 범위 기지모
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * -0.05f, collRange);

    }


    // Update is called once per frame
    void Update()
    {
        LookPlayer(targetPosition.position);
        MonsterState(targetPosition.position, slimeData.attackDistance, slimeData.attackSpeed, slimeData.attackMotionSpeed);
        UpdateState(targetPosition.position, slimeData.moveSpeed);
        
    }
}
