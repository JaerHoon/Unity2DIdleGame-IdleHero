using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform playerTr;
    Transform monsterTr;
    Animator anim;
    Rigidbody2D rb;

    [SerializeField]
    float speed;


    public GameObject explosion;
    float attackDistance = 3.0f;
    //bool isAttack = false;
    void Start()
    {
        playerTr = GameObject.Find("Player").GetComponent<Transform>();
        monsterTr = GameObject.Find("monster").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //pos = GameObject.Find("pos").GetComponentInChildren<Transform>();
        anim = GetComponent<Animator>();
        
    }

    public void ActiveSkill(Skill_ScriptableObject skill)
    {
        
        anim.Play(skill.animationName);
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(playerTr.position, monsterTr.position);

        if (distance < attackDistance)
        {
            anim.SetBool("attack", true);

        }
        else
        {
            anim.SetBool("attack", false);
        }

    }

  
}
