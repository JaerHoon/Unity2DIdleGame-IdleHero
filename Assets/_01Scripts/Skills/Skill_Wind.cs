using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Wind : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject Wind;
    float WindSpeed = 3.0f;

    [SerializeField]
    Image skillimage; // 스킬 아이콘 이미지

    [SerializeField]
    Image skillCoolTimeGauge; // 스킬 쿨타임 게이지 표시 이미지

    bool isCoolTime = false; // 쿨타임 플래그
    float skillCoolTime = 0f; // 초기 쿨타임값
    float maxskillCool; // 최대 쿨타임값
    public float rotateSpeed = 30.0f;

    Vector2[] dir = { Vector2.up + Vector2.right, Vector2.down+Vector2.left,
                      Vector2.up + Vector2.left, Vector2.down + Vector2.right }; // 각 대각선 방향으로 스킬이 나간다.

    
    void Start()
    {
        skillimage.sprite = Wind.icon; // 스킬이미지 스프라이트를 스크립터블 오브젝트에 넣은 아이콘 스프라이트로 표시
        maxskillCool = Wind.coolTime; // 최대 쿨타임 = 스크립터블 오브젝트에서 작성한 쿨타임
        skillCoolTimeGauge.fillAmount = 0f;

        
    }

    public void WindShoot()
    {
        if (isCoolTime) // 쿨타임중일때는 공격X
        {
            return;
        }

        foreach (Vector2 direction in dir)
        {
            GameObject wind = Instantiate(Wind.skillPrefab, transform.position, Quaternion.identity);
            CoolTimeStart(); // 스킬 발동되고 나서 쿨타임 시작
            Rigidbody2D rb = wind.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * WindSpeed;
            

        }

        


    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = Wind.coolTime;

    }

    public void CoolTimeState()
    {
        float cooltime = skillCoolTime / maxskillCool; // 스킬 쿨타임 / 최대쿨타임
        skillCoolTimeGauge.fillAmount = cooltime;
    }

    
    
    // Update is called once per frame
    void Update()
    {
        if (isCoolTime)
        {
            skillCoolTime -= Time.deltaTime; // Time값 만큼 쿨타임 조금씩 감소한다.
            CoolTimeState();
            if (skillCoolTime <= 0f)
            {
                isCoolTime = false;

            }
        }

        


    }
}

