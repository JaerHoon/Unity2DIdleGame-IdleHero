using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform playerTr; // 플레이어 위치
    public Transform pos; // 공격 인식할 위치
    public Transform DownPos;
    Animator anim;
    Rigidbody2D rb;
    Transform minDisMon;

    public LayerMask layermask;
    float range = 1.0f;
    int monsterLayer;
    

    float attackDistance = 3.0f;
    public bool isAttack = false;
    public bool isMove;

    float scaleX;
    float scaleY;
    float scaleZ;

    [SerializeField]
    Vector2 boxSize;
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //monsterTr = GameObject.FindWithTag("monster").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //pos = GameObject.Find("pos").GetComponentInChildren<Transform>();
        anim = GetComponent<Animator>();

        monsterLayer = LayerMask.NameToLayer("monster");
        layermask = 1 << monsterLayer;

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;

    }
    public void IsMonsterAttack()
    {
        //Vector2 plusPos = new Vector2(pos.position.x + 1.3f, pos.position.y + 1.3f);
        //Vector2 size = new Vector2(1.2f, 2.0f);
        Collider2D[] AttackArea = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        foreach (Collider2D colls in AttackArea)
        {
            if(colls.CompareTag("monster"))
            {
                isAttack = true;
                attackAnim();
            }
            
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos.position, range);

        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos.position, boxSize);
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

            minDisMon = null;
            distance = 10000;
            
           


        }
       
    }

   
    void attackAnim()
    {
        anim.SetInteger("attack", 1); // 공격 애니메이션 동작
    }

    void stopAttackAnim()
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
        IsMonsterAttack();
    }

}
