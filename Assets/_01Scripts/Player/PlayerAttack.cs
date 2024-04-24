using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform playerTr;
    Transform monsterTr;
    Animator anim;

    float attackDistance = 3.0f;
    //bool isAttack = false;
    void Start()
    {
        playerTr = GameObject.Find("Player").GetComponent<Transform>();
        monsterTr = GameObject.Find("monster").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        
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
