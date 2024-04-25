using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour
{
    /*public PlayerAttack player;
    public Skill_ScriptableObject skill;

    public Image imgCool;
    public Image imgIcon;
    void Start()
    {
        imgIcon.sprite = skill.icon;
        imgCool.fillAmount = 0;
    }

    public void OnClicked()
    {
        if(imgCool.fillAmount >0)
        {
            return;
        }
        player.ActiveSkill(skill);

        StartCoroutine(skillCollTime());
    }

    IEnumerator skillCollTime()
    {
        float tick = 1f / skill.coolTime;
        float t = 0;

        imgCool.fillAmount = 1;

        while(imgCool.fillAmount >0)
        {
            imgCool.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            yield return null;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
