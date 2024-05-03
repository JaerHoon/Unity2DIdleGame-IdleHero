using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Meteor : MonoBehaviour
{
    
    [SerializeField]
    Skill_ScriptableObject meteor; 

    [SerializeField]
    Image skillimage; // 스킬 아이콘 이미지

    [SerializeField]
    Image skillCoolTimeGauge; // 스킬 쿨타임 게이지 표시 이미지

    bool isCoolTime = false; // 쿨타임 플래그
    float skillCoolTime = 0f; // 초기 쿨타임값
    float maxskillCool; // 최대 쿨타임값

    int skillNumber = 4;
    float nextSkillTime = 0.5f;
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
        SkillManager.instance.OnBigMeteorAttack(); // 스킬매니저에서 만든 함수를 불러온다.
        CoolTimeStart();



    }

    IEnumerator MeteorCopy()
    {
        int usedMeteor = 0;

        while (usedMeteor < skillNumber)
        {
            usedMeteor++;
            yield return new WaitForSeconds(nextSkillTime);
        }

        CoolTimeStart();
        for (int i = 0; i < skillNumber; i++)
        {
            SkillManager.instance.OnMeteorAttack();
            yield return new WaitForSeconds(nextSkillTime);


        }
        yield return new WaitForSeconds(0.6f);
        SkillManager.instance.OnBigMeteorAttack();


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
