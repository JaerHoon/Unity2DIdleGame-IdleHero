using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    [SerializeField]
    Player_ScriptableObject player;
    
    public int PlayerDamage;
    Skill_Buff skillBuff;
    public int playerAttackDamage()
    {
        int damage = 0;

        int atkpow = StatusManager.instance.GetStatus(StatusManager.playerATkpow);
        int CrtRate = StatusManager.instance.GetStatus(StatusManager.playerCrtRate) /10;
        int Rnum = Random.Range(1, 101);

        damage =  atkpow + skillBuff._buffAttackCal();
        
        if(Rnum < CrtRate)
        {
            damage *= player.playerCriticalPower;
        }
        return damage;
    }

   

    private void Start()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        skillBuff = GameObject.Find("SkillManager").GetComponent<Skill_Buff>();
    }

    public void OnColliderBox()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        PlayerSound.instance.OnwpaponSound();
        Invoke("offCollider", 0.2f);
    }

    public void offCollider()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("monster"))
        {

            collision.gameObject.GetComponent<RecyclableMonster>().OnMonDamaged(playerAttackDamage());
        }

    }
}
