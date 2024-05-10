using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    [SerializeField]
    Player_ScriptableObject player;
    
    public int PlayerDamage;
    
    public void playerAttackDamage()
    {
        int atkpow = StatusManager.instance.GetStatus(StatusManager.playerATkpow);
        int CrtRate = StatusManager.instance.GetStatus(StatusManager.playerCrtRate) /10;
        int Rnum = Random.Range(1, 101);
        
        if(Rnum < CrtRate)
        {
            PlayerDamage = player.playerDamage + (int)(player.playerDamage * player.playerCriticalPower);
            
        }
        else
        {
            PlayerDamage = player.playerDamage + atkpow;
        }
    }

    private void Start()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
            if (collision.gameObject.GetComponent<Slime>() != null)
                collision.gameObject.GetComponent<Slime>().OnMonDamaged(PlayerDamage);
            else if (collision.gameObject.GetComponent<Spider>() != null)
                collision.gameObject.GetComponent<Spider>().OnMonDamaged(PlayerDamage);
            else if (collision.gameObject.GetComponent<Bat>() != null)
                collision.gameObject.GetComponent<Bat>().OnMonDamaged(PlayerDamage);
            else if (collision.gameObject.GetComponent<BabyDragon>() != null)
                collision.gameObject.GetComponent<BabyDragon>().OnMonDamaged(PlayerDamage);

            
        }

    }
}
