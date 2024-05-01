using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Meteor : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject meteor;
    float meteorSpeed = 3.0f;

    [SerializeField]
    Image skillimage; // 스킬 아이콘 이미지

    [SerializeField]
    Image skillCoolTimeGauge; // 스킬 쿨타임 게이지 표시 이미지

    bool isCoolTime = false; // 쿨타임 플래그
    float skillCoolTime = 0f; // 초기 쿨타임값
    float maxskillCool; // 최대 쿨타임값

    float fixTime = 0f;
    void Start()
    {
        skillimage.sprite = meteor.icon; // 스킬이미지 스프라이트를 스크립터블 오브젝트에 넣은 아이콘 스프라이트로 표시
        maxskillCool = meteor.coolTime; // 최대 쿨타임 = 스크립터블 오브젝트에서 작성한 쿨타임
        skillCoolTimeGauge.fillAmount = 0f;

        
    }

    public void meteorShoot()
    {
        if (isCoolTime) // 쿨타임중일때는 공격X
        {
            return;
        }
        GameObject Meteor = Instantiate(meteor.skillPrefab);
        int meteorPos = Random.Range(-7, 8);
        Meteor.transform.position = new Vector2(meteorPos, 3.0f);
        Meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);
    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = meteor.coolTime;

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
