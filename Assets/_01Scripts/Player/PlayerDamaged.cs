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

    Skill_Buff skillBuff;
    public int playerhp;
    bool isBlood = false;
    int maxHP;
    
    float time;
    public float fadeTime = 1f;
    PlayerAttack stopAttack;

    private void Start()
    {
        skillBuff = GameObject.Find("SkillManager").GetComponent<Skill_Buff>();
        stopAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        Init();
    }

    public void Init()
    {
        isBlood = false;
        maxHP = StatusManager.instance.GetStatus(StatusManager.playerHP);
        playerhp = maxHP;
       
        hpImage.fillAmount = (float)playerhp / (float)maxHP;
    }


    public int DefenceCaculate()
    {
        int defence = 0;

        int def = StatusManager.instance.GetStatus(StatusManager.playerDefence);
        defence = def + skillBuff._buffDefecneCal();

        return defence;
    }

    public void HpCaculate(int UPAmount)
    {  
        maxHP = StatusManager.instance.GetStatus(StatusManager.playerHP);
        playerhp += UPAmount;
        hpImage.fillAmount = (float)playerhp / (float)maxHP;
    }

    public void OnPlayerDamaged(int monDamage)
    {

        GameObject Text = Instantiate(DamageText);
        Text.transform.position = TextPos.position;
        
        if(monDamage- DefenceCaculate() > 0)
        {
            //isPlayerDamage = true;
            playerhp -= (int)monDamage - DefenceCaculate();


            Text.GetComponent<PlayerDamageText>().Damage = monDamage - DefenceCaculate();


        }
        else
        {
            monDamage = 0;
            Text.GetComponent<PlayerDamageText>().Damage = 0;
        }
        
        hpImage.fillAmount = (float)playerhp / (float)maxHP;
        
        //print("플레이어가 공격 받았습니다!! 데미지 : "+ monDamage);

        if(playerhp <=0 && !isBlood)
        {
            StageManager.instance.OnPlayerDie();
            GameObject blood = Instantiate(dieEffect);
            blood.transform.position = transform.position + Vector3.up * 0.5f;
            isBlood = true;
           // this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(blood, 0.5f);

            playerfadeTime();
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
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
        }
    }
    void Update()
    {
        //if(playerhp <= 0)
        //{
        //    time += Time.deltaTime;
        //    Invoke("playerfadeTime", 0.6f);
        //}

       
    }
    
}
