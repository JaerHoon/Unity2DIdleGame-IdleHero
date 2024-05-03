using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_tornado : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject tornado;
    
    [SerializeField]
    Image skillimage; // 스킬 아이콘 이미지

    [SerializeField]
    Image skillCoolTimeGauge; // 스킬 쿨타임 게이지 표시 이미지

    bool isCoolTime = false; // 쿨타임 플래그
    float skillCoolTime = 0f; // 초기 쿨타임값
    float maxskillCool; // 최대 쿨타임값

    
    void Start()
    {
        skillimage.sprite = tornado.icon; // 스킬이미지 스프라이트를 스크립터블 오브젝트에 넣은 아이콘 스프라이트로 표시
        maxskillCool = tornado.coolTime; // 최대 쿨타임 = 스크립터블 오브젝트에서 작성한 쿨타임
        skillCoolTimeGauge.fillAmount = 0f;
    }

    public void tornadoShoot()
    {
        if(isCoolTime)
        {
            return;
        }

        SkillManager.instance.OnTornadoAttack();
        CoolTimeStart();


    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = tornado.coolTime;

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
