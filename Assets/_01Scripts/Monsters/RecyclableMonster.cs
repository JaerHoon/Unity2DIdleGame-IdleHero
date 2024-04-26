using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecyclableMonster : MonoBehaviour
{
    protected enum STATE { IDEL, TRACE, ATTACK,DAMAGED, DIE }
    //트리거 0 : idle, 1 : walk, 2 : attack, 3 : hurt, 4 : dead, 5 : dragonfall
    //================선언=============================
    protected Vector3 targetPosition;//플레이어의 위치
    protected bool isDead = false;
    protected bool isActivated = false;
    protected bool isCanAttack = true;
    protected bool isAttacking = false;
    protected bool isDamaged = false;
    protected float lastAttackTime = 0;
    protected float DamagedTime = 0.3f;
    protected STATE state { get; set; }
    


    //================이벤트==========================
    public Action MonDeath;


    //================================================



    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPosition = Vector3.zero;//플레이어 위치 초기화
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //몬스터 생성 위치 설정
    public virtual void Activate(Vector3 spawnPos)
    {
        isActivated = true;//활성 플래그 참
        transform.position = spawnPos;//스폰 포인트로 위치 전환
    }

    //플레이어의 방향으로 y축 회전
    public virtual void LookPlayer(Vector3 playerPos)
    {

        if (playerPos.x >= transform.position.x)//플레이어가 몬스터보다 오른쪽에 있으면
        {
            if (transform.rotation.y == 180)//y값이 180일때 오른쪽을 보는 몬스터의 경우
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180.0f, transform.rotation.z);
            else//y값이 0일때 오른쪽을 보는 몬스터의 경우
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        else//플레이어가 몬스터보다 왼쪽에 있으면
        {
            if (transform.rotation.y == 180)
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            else
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180.0f, transform.rotation.z);
        }



    }


    //몬스터 사정거리까지 이동 함수
    public virtual void MonsterState(Vector3 playerPos, float attackDistance, float attackSpeed, float motionSpeed)
    {
        
        float distance = Vector3.Distance(playerPos, transform.position);

        

        if (!isDead)//죽지 않았다면
        {
            if (!isDamaged)//공격을 받은 상태가 아니라면
            {
                if (!isAttacking)//공격중이 아니면
                {
                    if (distance <= attackDistance)//사정거리보다 가깝다면
                    {
                        if (isCanAttack)//공격 가능 상태라면
                        {
                            state = STATE.ATTACK;//공격 상태
                            StartCoroutine(DelayAttack(attackSpeed));//공격속도 시간 후 공격가능
                            StartCoroutine(DelayAttackMotion(motionSpeed));//공격속도 시간 후 공격가능
                            isAttacking = true;//공격중
                            isCanAttack = false;//이미 공격 중이므로 공격 불가
                        }
                        else//공격 가능 상태가 아니라면
                        {
                            state = STATE.IDEL;
                        }
                    }
                    else if (distance > attackDistance)
                    {
                        state = STATE.TRACE;
                    }
                }
                else
                    state = STATE.ATTACK;
            }
            else
            {
                state = STATE.DAMAGED;
            }
        }
    }

    


    public virtual int MonDamaged(int MonHp,int MonDef,int PlayerDamage)//몬스터 피격 함수 계산 후 Hp 배출
    {
        isDamaged = true;
        return MonHp - (PlayerDamage >= MonDef ? PlayerDamage - MonDef : 0);
    }

    IEnumerator DelayDamaged(float DamagedTime)
    {
        yield return new WaitForSeconds(DamagedTime);
        isDamaged = false;
    }

    IEnumerator DelayAttack(float attackSpeed)//공격 딜레이 코루틴
    {
        yield return new WaitForSeconds(attackSpeed);
        isCanAttack = true;
        
    }

    IEnumerator DelayAttackMotion(float Motion)//공격 딜레이 코루틴
    {
        yield return new WaitForSeconds(Motion);
        
        isAttacking = false;
    }

    public virtual void UpdateState(Vector3 playerPos, float moveSpeed)
    {
        switch (state)
        {
            case STATE.IDEL:
                IdleState();
                break;
            case STATE.TRACE:
                TraceState(playerPos, moveSpeed);
                break;
            case STATE.ATTACK:
                AttackState();
                break;
            case STATE.DAMAGED:
                DamagedState();
                break;
            case STATE.DIE:
                DieState();
                break;
            default:
                break;
        }

    }

    public virtual void IdleState()
    {

    }
    public virtual void TraceState(Vector3 playerPos, float moveSpeed)
    {
        MonsterMovement(playerPos, moveSpeed);
    }
    public virtual void AttackState()
    {

    }
    public virtual void DamagedState()
    {

    }
    public virtual void DieState()
    {

    }

    public virtual void MonsterMovement(Vector3 playerPos, float moveSpeed)//몬스터 이동
    {
        Vector3 dir = (playerPos - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }



}