using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterRecycle : MonoBehaviour
{
    //================선언=============================
    protected Vector3 targetPosition;//플레이어의 위치
    protected bool isActivated = false;
    

    //================이벤트==========================
    public Action<MonsterRecycle> MonsterDeath;


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
                transform.rotation = Quaternion.Euler(transform.rotation.x,180.0f,transform.rotation.z);
            else//y값이 0일때 오른쪽을 보는 몬스터의 경우
                transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
        }
        else//플레이어가 몬스터보다 왼쪽에 있으면
        {
            if (transform.rotation.y == 180)
                transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
            else 
                transform.rotation = Quaternion.Euler(transform.rotation.x,180.0f,transform.rotation.z);
        }


       
    }

    public virtual void MoveToPlayer(Vector3 playerPos)
    {
        Vector3 dir = (playerPos - transform.position).normalized;

    }
   
}
