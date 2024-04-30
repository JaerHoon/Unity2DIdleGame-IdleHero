using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Wind : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject Wind; // 스크립터블 오브젝트 Wind 선언
    

    [SerializeField]
    Image skillimage; // 스킬 아이콘 이미지

    [SerializeField]
    Image skillCoolTimeGauge; // 스킬 쿨타임 게이지 표시 이미지

    bool isCoolTime = false; // 쿨타임 플래그
    float skillCoolTime = 0f; // 초기 쿨타임값
    float maxskillCool; // 최대 쿨타임값
  

    Vector2[] dir = { Vector2.up + Vector2.right, Vector2.down+Vector2.left,
                      Vector2.up + Vector2.left, Vector2.down + Vector2.right }; // 각 대각선 방향으로 스킬이 나간다.

    
    float[] angles; // Wind의 회전값을 배열로 할당
    void Start()
    {
        skillimage.sprite = Wind.icon; // 스킬이미지 스프라이트를 스크립터블 오브젝트에 넣은 아이콘 스프라이트로 표시
        maxskillCool = Wind.coolTime; // 최대 쿨타임 = 스크립터블 오브젝트에서 작성한 쿨타임
        skillCoolTimeGauge.fillAmount = 0f;

        
        angleWind();
    }

    

    public void angleWind() // 각 배열에 Rotation값을 할당해 놓았다.
    {
        angles = new float[4];

        
        angles[0] = 30f;
        angles[1] = 150f;
        angles[2] = -150f;
        angles[3] = -30f;
    }

    public void WindShoot()
    {
        if (isCoolTime) // 쿨타임중일때는 공격X
        {
            return;
        }

        
        for (int i = 0; i < dir.Length; i++)
        {
            Vector2 pos = transform.position; // 스킬 발사되는 위치 => 플레이어 따라오게 하기 위함
            GameObject wind = Instantiate(Wind.skillPrefab, pos, Quaternion.identity);
            CoolTimeStart(); // 스킬 발동됬을 때 쿨타임이 돌아간다.

            // 자연스럽게 대각선으로 발사되는 스킬을 만들기 위해 Vector2 배열값에 위에서 선언한 angles 배열을 할당한다.
            // ↖↗
            // ↙↘  <= 이런모양으로 발사되게 만들기 위해서 각도를 설정.
            wind.transform.rotation = Quaternion.Euler(0, 0, angles[i]); 
                                                                         
            
            
        }

        


    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = Wind.coolTime; // 스킬 쿨타임 값을 스크립터블 오브젝트에서 설정한 Wind 쿨타임 값으로 만든다.

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

