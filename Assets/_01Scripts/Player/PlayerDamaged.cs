using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    [SerializeField]
    Player_ScriptableObject player;
    [SerializeField]
    GameObject DamageText;
    [SerializeField]
    Transform TextPos;

    int playerhp;
    bool isPlayerDamage = false;
    void Start()
    {
        playerhp = player.playerHP;
        
    }

    

    public void OnPlayerDamaged(int monDamage)
    {

        if(isPlayerDamage)
        {
            return;
        }

        isPlayerDamage = true;
        playerhp -= monDamage;
        print("HP : " + player.playerHP);

        GameObject Text = Instantiate(DamageText);
        Text.transform.position = TextPos.position;
        Text.GetComponent<PlayerDamageText>().Damage = monDamage;
        StartCoroutine(StartPlayerDamage());
        //print("플레이어가 공격 받았습니다!! 데미지 : "+ monDamage);

        if (playerhp <= 0)
        {
            playerhp = player.playerHP;
        }
        
        
    }
    
    IEnumerator StartPlayerDamage()
    {
        yield return new WaitForSeconds(0.5f);
        isPlayerDamage = false;
    }
    void Update()
    {
        
    }
}
