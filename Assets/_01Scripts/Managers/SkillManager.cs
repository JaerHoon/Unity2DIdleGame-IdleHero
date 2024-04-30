using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject destroy;

    void Start()
    {
        
    }

    public void destroySkill()
    {
        Destroy(gameObject, destroy.Destroyskill);
    }

    // Update is called once per frame
    void Update()
    {
        destroySkill();
    }
}
