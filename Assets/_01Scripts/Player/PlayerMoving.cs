using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerMoving : MonoBehaviour
{
    public VariableJoystick joy;
    Animator anim;
    

    float scaleX;
    float scaleY;
    float scaleZ;
    public int speed;

    PlayerAttack playerattack;
    private void Start()
    {
        
        anim = GetComponent<Animator>();
        playerattack = GetComponent<PlayerAttack>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    void FixedUpdate()
    {
        float x = joy.Horizontal; // 좌우 움직임
        float y = joy.Vertical; // 상하 움직임
        Vector3 pos = new Vector3(x, y, 0); // 방향 설정
        pos.Normalize(); // 벡터 크기 정규화해서 일정한 속도를 나타낼 수 있음

        transform.position += pos * speed * Time.deltaTime;

        if (joy.Horizontal >0)
        {
            playerattack.isMove = true;
            anim.SetTrigger("run"); // 캐릭터 이동 애니메이션
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
        else if(joy.Horizontal < 0)
        {
            playerattack.isMove = true;
            anim.SetTrigger("run");
            transform.localScale = new Vector3(-scaleX, scaleY, scaleZ); // 반대방향 바라보게 하기 위함
        }
        else
        {
            playerattack.isMove = false;
            anim.SetTrigger("idle");
        }

        
       
    }
}
