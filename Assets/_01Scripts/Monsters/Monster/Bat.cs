using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bat : RecyclableMonster
{
    [SerializeField]
    MonsterData batData;
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
        monName = batData.monsterName;
        hp = batData.hp;
        damage = batData.damage;
        defense = batData.defense;
        moveSpeed = batData.moveSpeed;
        attackDistance = batData.attackDistance;
        attackSpeed = batData.attackSpeed;
        attackMotionSpeed = batData.attackMotionSpeed;
        if (MyRenderer != null) SetAlpa();
        if (Mycollider2D != null) Mycollider2D.enabled = true;
        transform.GetChild(0).localPosition = new Vector2(0,- 0.16f);
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
            StartCoroutine(DelayDamaged(0.5f));
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
        anim.SetInteger("STATE", 0);
    }
    public override void DamagedState()
    {
        base.DamagedState();
        print("박쥐 피격");
        anim.SetInteger("STATE", 3);
    }
    public override void DieState()
    {
        base.DieState();
        anim.SetInteger("STATE", 4);
        if(!isOnceDieState)
            Fall();
    }

    void Fall()
    {
        isOnceDieState = true;
        transform.DOMoveY(transform.position.y - 0.4f, 1f).SetEase(Ease.OutBounce);
        transform.GetChild(0).DOMoveY(transform.position.y - 0.65f,1).SetEase(Ease.OutBounce);
        //batTest.UpShadow();
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
        MonsterState(targetPosition.position, batData.attackDistance, batData.attackSpeed, batData.attackMotionSpeed);
        UpdateState(targetPosition.position, batData.moveSpeed);
    }
}
