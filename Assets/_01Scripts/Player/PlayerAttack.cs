using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform playerTr;
    public Transform monsterTr;
    Animator anim;
    Rigidbody2D rb;

    


    
    float attackDistance = 3.0f;
    
    //bool isAttack = false;
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //monsterTr = GameObject.FindWithTag("monster").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //pos = GameObject.Find("pos").GetComponentInChildren<Transform>();
        anim = GetComponent<Animator>();

        anim.SetBool("attack", false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "monster")
        {
            anim.SetInteger("attack", 1);
        }
       

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "monster")
        {
            anim.SetInteger("attack", 0);
        }

        
    }


    /*public void ActiveSkill(Skill_ScriptableObject skill)
    {
        
        anim.Play(skill.animationName);
    }*/


    // Update is called once per frame
    void Update()
    {
       
    }

  
}
