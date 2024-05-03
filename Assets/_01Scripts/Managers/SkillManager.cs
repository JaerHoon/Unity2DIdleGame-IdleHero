using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        if (SkillManager.instance == null)
            SkillManager.instance = this;
    }


    [SerializeField]
    Skill_ScriptableObject Earth;

    [SerializeField]
    Skill_ScriptableObject Wind;

    [SerializeField]
    Skill_ScriptableObject Tornado;

    [SerializeField]
    Skill_ScriptableObject Meteor;

    [SerializeField]
    GameObject EarthPrefab;

    [SerializeField]
    GameObject WindPrefab;

    [SerializeField]
    GameObject TornadoPrefab;

    [SerializeField]
    GameObject MeteorPrefab;

    [SerializeField]
    Transform playerTr;

    SkillFactory skillfactory;
    SkillFactory earthFactory;
    SkillFactory windFactory;
    SkillFactory tornadoFactory;
    SkillFactory meteorFactory;

    public Transform earthPos;

    float tornadoSpeed = 5.0f;
    
    Vector2[] Tornadodir = { Vector2.up, Vector2.down, Vector2.right, Vector2.left, Vector2.down+Vector2.left,
                      Vector2.up+Vector2.right, Vector2.up+Vector2.left, Vector2.down+Vector2.right};

    Vector2[] Winddir = { Vector2.up + Vector2.right, Vector2.down+Vector2.left,
                      Vector2.up + Vector2.left, Vector2.down + Vector2.right }; // 각 대각선 방향으로 스킬이 나간다.
    float[] angles; // Wind의 회전값을 배열로 할당

    int skillNumber = 3;
    float nextSkillTime = 1.5f;
    void Start()
    {
        earthFactory = new SkillFactory(EarthPrefab, 1);
        windFactory = new SkillFactory(WindPrefab, 4);
        tornadoFactory = new SkillFactory(TornadoPrefab, 8);
        meteorFactory = new SkillFactory(MeteorPrefab, 1);

        angleWind();
        StartCoroutine(MeteorCopy());


    }

    IEnumerator MeteorCopy()
    {
        int usedMeteor = 0;
        
        while (usedMeteor < skillNumber)
        {
            OnMeteorAttack();
            usedMeteor++;
            yield return new WaitForSeconds(nextSkillTime);
        }
        
    }

    void angleWind() // 각 배열에 Rotation값을 할당해 놓았다.
    {
        angles = new float[4];


        angles[0] = 30f;
        angles[1] = 150f;
        angles[2] = -150f;
        angles[3] = -30f;
    }


    public void OnEarthAttack()
    {
        GameObject earth = earthFactory.GetSkill();
        earth.transform.position = earthPos.position;
    }

    public void OnTornadoAttack()
    {
   
        foreach (Vector2 direction in Tornadodir)
        {
            GameObject tornado = tornadoFactory.GetSkill();
            tornado.transform.position = playerTr.position;
            Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * tornadoSpeed;

        }
        
        
    }

    public void OnWindAttack()
    {
        for (int i = 0; i < Winddir.Length; i++)
        {
            GameObject wind = windFactory.GetSkill();
            wind.transform.position = playerTr.position;
            wind.transform.rotation = Quaternion.Euler(0, 0, angles[i]);
        }
        
    }

    public void OnMeteorAttack()
    {
        GameObject meteor = meteorFactory.GetSkill();
        meteor.transform.position = new Vector2(7, 7);
        meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);
        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
