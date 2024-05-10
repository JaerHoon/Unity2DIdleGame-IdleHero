using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDamaged : MonoBehaviour
{
    [SerializeField]
    Player_ScriptableObject player;
    [SerializeField]
    GameObject DamageText;
    [SerializeField]
    Transform TextPos;
    [SerializeField]
    GameObject dieEffect;
    [SerializeField]
    Image hpImage;
  

    int playerhp;
    bool isPlayerDamage = false;
    bool isBlood = false;
    int playerDefence;
    int maxHP;
    
    float time;
    public float fadeTime = 1f;
    void Start()
    {
        playerhp = player.playerHP;
        maxHP = playerhp;
    }

    public void DefenceCaculate()
    {
        playerDefence = StatusManager.instance.GetStatus(StatusManager.playerDefence);
        
    }

    public void HpCaculate()
    {
        int hp = StatusManager.instance.GetStatus(StatusManager.playerHP);
        playerhp += hp;
        maxHP = player.playerHP + hp;
        
    }

    public void OnPlayerDamaged(int monDamage)
    {
        
        if(monDamage-playerDefence >0)
        {
            //isPlayerDamage = true;
            playerhp -= (int)monDamage - playerDefence;
            print("HP : " + playerhp);
            
            


        }
        else
        {
            monDamage = 0;
        }
        
        hpImage.fillAmount = (float)playerhp / (float)maxHP;
        GameObject Text = Instantiate(DamageText);
        Text.transform.position = TextPos.position;
        Text.GetComponent<PlayerDamageText>().Damage = monDamage;
        //print("플레이어가 공격 받았습니다!! 데미지 : "+ monDamage);

        if(playerhp <=0 && !isBlood)
        {
            GameObject blood = Instantiate(dieEffect);
            blood.transform.position = transform.position + Vector3.up * 0.5f;
            isBlood = true;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(blood, 0.5f);
            
        }


    }

    

    void playerfadeTime()
    {
        if(time < fadeTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f - time / fadeTime);
        }
        else
        {
            time = 0;
            this.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if(playerhp <= 0)
        {
            time += Time.deltaTime;
            Invoke("playerfadeTime", 0.6f);
        }
    }
    
}
