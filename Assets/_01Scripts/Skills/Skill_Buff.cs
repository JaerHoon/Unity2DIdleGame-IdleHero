using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Buff : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject buffForth;

    [SerializeField]
    Skill_ScriptableObject buffBack;

    [SerializeField]
    Image skillimage; // 스킬 아이콘 이미지

    [SerializeField]
    Image skillCoolTimeGauge; // 스킬 쿨타임 게이지 표시 이미지

    [SerializeField]
    Image buffState;

    bool isCoolTime = false; // 쿨타임 플래그
    float skillCoolTime = 0f; // 초기 쿨타임값
    float maxskillCool; // 최대 쿨타임값
    bool isBuffRun = false;

    SpriteRenderer player; // 스프라이트렌더러 변수
    float changeColorTime = 20.0f; // 플레이어 색상을 20초간 변경한다.
    float buffTime = 10.0f;
    public Color skillColor = new Color(1.0f, 0.5f, 0.5f, 1.0f); // 차례대로 R,G,B,알파값이며 red색상으로 변경한다.
    Color originColor; 
    void Start()
    {
        skillimage.sprite = buffForth.icon; // 스킬이미지 스프라이트를 스크립터블 오브젝트에 넣은 아이콘 스프라이트로 표시
        maxskillCool = buffForth.coolTime; // 최대 쿨타임 = 스크립터블 오브젝트에서 작성한 쿨타임
        skillCoolTimeGauge.fillAmount = 0f;

        skillimage.sprite = buffBack.icon; // 스킬이미지 스프라이트를 스크립터블 오브젝트에 넣은 아이콘 스프라이트로 표시
        maxskillCool = buffBack.coolTime; // 최대 쿨타임 = 스크립터블 오브젝트에서 작성한 쿨타임
        skillCoolTimeGauge.fillAmount = 0f;

        player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        originColor = player.color; // 플레이어의 원래 색상을 originColor에 담는다.

        buffState.enabled = false;
    }

    public void ActivatedBuff()
    {
        if (isCoolTime) // 쿨타임중일때는 공격X
        {
            return;
        }

        SkillManager.instance.OnActivatedBuff();
        CoolTimeStart();
        StartCoroutine(changeColor(skillColor, changeColorTime));
        
        buffState.enabled = true;
        StartCoroutine(currentBuff());


    }

    IEnumerator changeColor(Color redColorChange, float changeTime)
    {
        player.color = redColorChange; // 버프 눌렀을 때 플레이어 색상을 위에서 선언한 skillColor로 변경한다.
        yield return new WaitForSeconds(changeTime); // changeColorTime만큼 대기한다.
        player.color = originColor; // changeColorTime 이후에는 플레이어의 색상을 원래 색상으로 되돌린다.

        
    }


    IEnumerator currentBuff()
    {
        yield return new WaitForSeconds(0.5f);
        buffState.enabled = false;
        yield return new WaitForSeconds(0.5f);
        buffState.enabled = true;
        StartCoroutine(currentBuff());

    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = buffForth.coolTime;
        skillCoolTime = buffBack.coolTime;

    }

    public void CoolTimeState()
    {
        float distance = skillCoolTime / maxskillCool; // 스킬 쿨타임 / 최대쿨타임
        skillCoolTimeGauge.fillAmount = distance;
        //print(buffCoolTimeState);
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
