using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecyclableMonster : MonoBehaviour
{
    protected enum STATE { IDEL, TRACE, ATTACK, DIE }
    //================선언=============================
    protected Vector3 targetPosition;//플레이어의 위치
    protected bool isDead = false;
    protected bool isActivated = false;
    protected bool isCanAttack = false;
    protected bool isAttacking = false;
    protected float lastAttackTime = 0;
    protected STATE state { get; set; }


    //================이벤트==========================
    public Action<RecyclableMonster> MonsDeath;
    public event Action MonAttack;


    //================================================



    // Start is called before the first frame update
    void Start()
    {
        isCanAttack = true;
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
    public virtual void MonsterState(Vector3 playerPos, float attackDistance, float attackSpeed)
    {
        
        float distance = Vector3.Distance(playerPos, transform.position);

        

        if (!isDead)//죽지 않았다면
        {

            if (!isAttacking)
            {
                if (distance <= attackDistance && isCanAttack)//사정거리보다 가깝다면, 공격가능 상태라면
                {
                    isCanAttack = false;
                    isAttacking = true;
                    StartCoroutine(DelayAttack(attackSpeed));//공격속도 시간 후 공격가능
                    state = STATE.ATTACK;
                }
                else if (distance <= attackDistance && !isCanAttack) //사정거리보다 가깝다면, 공격불가 상태라면
                {
                    state = STATE.IDEL;
                }
                else if (distance > attackDistance && !isCanAttack)
                {
                    state = STATE.TRACE;
                }
            }
            else
                state = STATE.ATTACK;
        }
    }

    IEnumerator DelayAttack(float attackSpeed)//공격 딜레이 코루틴
    {
        yield return new WaitForSeconds(attackSpeed);
        isCanAttack = true;
        isAttacking = false;
    }

    public virtual void UpdateState(Vector3 playerPos, int moveSpeed)
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
    public virtual void TraceState(Vector3 playerPos, int moveSpeed)
    {
        MonsterMovement(playerPos, moveSpeed);
    }
    public virtual void AttackState()
    {

    }
    public virtual void DieState()
    {

    }

    public virtual void MonsterMovement(Vector3 playerPos, int moveSpeed)//몬스터 이동
    {
        Vector3 dir = (playerPos - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }



}