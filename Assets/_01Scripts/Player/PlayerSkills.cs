using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSkills : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject skills;

    [SerializeField]
    Transform skilsPos;

    [SerializeField]
    Image skillimage;

    bool isCoolTime = false;
    float skillCoolTime = 0f;
    void Start()
    {
        skillimage.sprite = skills.icon;
    }

    public void SkillAttack()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(skills.skillPrefab, skilsPos.position, Quaternion.identity);
            CoolTimeStart();
            
        }
    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = skills.coolTime;
        skillimage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCoolTime)
        {
            skillCoolTime -= Time.deltaTime;
            if (skillCoolTime <= 0f)
            {
                isCoolTime = false;
                skillimage.enabled = true;
            }
        }

        print(skillCoolTime);

        if (isCoolTime)
            return;
        SkillAttack();
    }
}
