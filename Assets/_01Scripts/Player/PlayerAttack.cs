using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerAttack : MonoBehaviour
{
    public Transform playerTr; // 플레이어 위치
    public Transform pos; // 공격 인식할 위치
    Animator anim;
    Transform minDisMon;

    
    float range = 1.0f;
    

    public Action<int> monattack;

    int attackDamage = 5;
    public bool isAttack = false;
    public bool isMove;

    float scaleX;
    float scaleY;
    float scaleZ;

    [SerializeField]
    float boxSize;



    public LayerMask layermask;
    int monsterLayer;
    int flymonsterLayer;
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();

        monsterLayer = LayerMask.NameToLayer("monster");
        flymonsterLayer = LayerMask.NameToLayer("flymonster");

        layermask = 1 << monsterLayer | 1 << flymonsterLayer;

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;

    }

    public void GetAttackBox()
    {
        GetComponentInChildren<PlayerAttackBox>().OnColliderBox();
        //print("inGetAttackBox");
    }

    public void IsMonsterAttack()
    {
        //Vector2 plusPos = new Vector2(pos.position.x + 1.3f, pos.position.y + 1.3f);
        //Vector2 size = new Vector2(1.2f, 2.0f);
        Collider2D[] AttackArea = Physics2D.OverlapCircleAll(pos.position, boxSize, layermask);

        /*foreach (Collider2D colls in AttackArea)
        {
            if(colls.CompareTag("monster"))
            {
                print("ATK");
                isAttack = true;
                attackAnim();
                monattack?.Invoke(attackDamage);
            }
           
        }*/
        if(AttackArea.Length ==0)
        {
            isAttack = false;
            stopAttackAnim();

        }
        else if(AttackArea.Length > 0)
        {
            print("ATK");
            isAttack = true;
            attackAnim();
            monattack?.Invoke(attackDamage);
        }
        
        
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTr.position, range);

        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos.position, boxSize);
    }

 
    public void IsMonsterRecognition()
    {
        Collider2D[] RecognitionArea = Physics2D.OverlapCircleAll(playerTr.position, range, layermask, -100.0f,100.0f);
        

        if (RecognitionArea.Length > 0)
        {

            float distance = float.MaxValue; // float의 최댓값
            for (int i = 0; i < RecognitionArea.Length; i++)
            {
                if (distance > Vector2.Distance(playerTr.position,RecognitionArea[i].transform.position))
                {
                    distance = Vector2.Distance(playerTr.position,RecognitionArea[i].transform.position);
                    minDisMon = RecognitionArea[i].transform;
                    
                }
            }
           
            
            if(isMove==false)
            {
                if (minDisMon.position.x > playerTr.position.x)
                    transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                else
                    transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);


                
            }

            minDisMon = null; // minDisMon을 null로 만들어서 가까운 몬스터를 계속 인식하도록 하기 위함.
            //distance = float.MaxValue;




        }
       
    }

   
    void attackAnim()
    {
        if(isAttack==true)
            anim.SetInteger("attack", 1); // 공격 애니메이션 동작

        
    }

    public void stopAttackAnim()
    {
        anim.SetInteger("attack", 0); // 공격 애니메이션 멈춤
    }
   
    void Update()
    {
       
        IsMonsterRecognition();

        

    }

    private void FixedUpdate() // 정해진 시간 간격으로 호출됨 => Physics2D 쓰는 함수 계산할 때 용이함.
    {
        if (isMove == true) // 움직일 때 공격모션 멈추고 run 애니메이션으로 돌아가기
        {
           
            stopAttackAnim();
            return;
        }
        else
        {
            IsMonsterAttack();
        }
        
    }

}
