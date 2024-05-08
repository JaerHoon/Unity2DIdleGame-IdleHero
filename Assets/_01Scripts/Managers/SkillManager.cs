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
    Skill_ScriptableObject earth;


    [SerializeField]
    Skill_ScriptableObject tornado;


    [SerializeField]
    Skill_ScriptableObject wind;


    [SerializeField]
    Skill_ScriptableObject meteor;


    [SerializeField]
    GameObject EarthPrefab;

    [SerializeField]
    GameObject WindPrefab;

    [SerializeField]
    GameObject TornadoPrefab;

    [SerializeField]
    GameObject MeteorPrefab;

    [SerializeField]
    GameObject BigMeteorPrefab;

    [SerializeField]
    GameObject BuffForthPrefab;

    [SerializeField]
    GameObject BuffBackPrefab;

    [SerializeField]
    Transform playerTr;

    [SerializeField]
    Image skillimage;

    [SerializeField]
    Image skillCoolTimeGauge; // 스킬 쿨타임 게이지 표시 이미지

    [SerializeField]
    Image skillCoolTimeGauge2; // 스킬 쿨타임 게이지 표시 이미지

    SkillFactory skillfactory;
    SkillFactory earthFactory;
    SkillFactory windFactory;
    SkillFactory tornadoFactory;
    SkillFactory meteorFactory;
    SkillFactory BigmeteorFactory;
    //SkillFactory buffForthFactory;
    //SkillFactory buffBackFactory;
    public Transform earthPos;
    



    float tornadoSpeed = 5.0f;
    
    Vector2[] Tornadodir = { Vector2.up, Vector2.down, Vector2.right, Vector2.left, Vector2.down+Vector2.left,
                      Vector2.up+Vector2.right, Vector2.up+Vector2.left, Vector2.down+Vector2.right};

    Vector2[] Winddir = { Vector2.up + Vector2.right, Vector2.down+Vector2.left,
                      Vector2.up + Vector2.left, Vector2.down + Vector2.right }; // 각 대각선 방향으로 스킬이 나간다.
    float[] angles; // Wind의 회전값을 배열로 할당


    public GameObject[] Slot = new GameObject[3];
    public Image[] cooltimeObject = new Image[3];
    public Button[] button = new Button[3];

    int[] skillSlot = new int[3];//스킬 슬롯에 장착된 스킬 번호를 표시
    public Skill_ScriptableObject equipskills;
    public GameObject skillslot1;

    bool isEarthCoolTime = true;
    
    [SerializeField]
    GameObject[] slot0skillimages;
    [SerializeField]
    GameObject[] slot1skillimages;
    /*********************쿨타임 관련 변수****************************/
    bool isCoolTime = false; // 쿨타임 플래그
    float skillCoolTime = 0f; // 초기 쿨타임값
    float maxskillCool; // 최대 쿨타임값
    void Start()
    {
        earthFactory = new SkillFactory(EarthPrefab, 1); // Earth관련 팩토리 가져오기
        windFactory = new SkillFactory(WindPrefab, 4); // wind 관련 팩토리 가져오기
        tornadoFactory = new SkillFactory(TornadoPrefab, 8); // tornado 관련 팩토리 가져오기
        meteorFactory = new SkillFactory(MeteorPrefab, 1); 
        BigmeteorFactory = new SkillFactory(BigMeteorPrefab, 1); // meteor관련 팩토리 가져오기
        //buffForthFactory = new SkillFactory(BuffForthPrefab, 1);
        //buffBackFactory = new SkillFactory(BuffBackPrefab, 1);
        angleWind();

        

        BuffForthPrefab.SetActive(false); // 버프활성화 프리팹 시작할때 비활성화
        BuffBackPrefab.SetActive(false); // 버프활성화 프리팹 시작할때 비활성화
        skillSlot[0] = 1; // 0번 스킬 슬롯에 1번 스킬 발동
        skillSlot[1] = 3; // 1번 스킬 슬롯에 3번 스킬 발동
        skillSlot[2] = 5; // 2번 스킬 슬롯에 5번 스킬 발동
        
        skillCoolTimeGauge.fillAmount = 0f;
        skillCoolTimeGauge2.fillAmount = 0f;
        skillslot1 = GameObject.Find("skillslot1");

        //skillslot1.transform.GetChild(0).gameObject.SetActive(false);
        //skillslot1.transform.GetChild(1).gameObject.SetActive(true);
        //skillslot1.transform.GetChild(2).gameObject.SetActive(false);
        //skillslot1.transform.GetChild(3).gameObject.SetActive(false);

        

        //if (skillSlot[0] == 1)
        {
            //skillimage.sprite = earth.icon;
        }


    }

    void Oncooltime(int slotnum)
    {
        //Slot[slotnum]안에 들어있는 스킬 함수 실행
        // 스타트 코루틴(Slot[sslotnum].스킬쿨타임을매개변수1, cooltimeOBJ[slotnum])
        // button 을 꺼 
    }

    IEnumerator Startcooltimeg(float cooltime, int  slotnum)
    {
        yield return new WaitForSeconds(0.1f);
        //cooltimeObject[slotnum]

        button[slotnum].enabled = true;
        //쿨타임 끝나면 다시 버튼을 켜.
    }

    public void OnclickSkill(int slotNumber)
    {
        if (slotNumber == 0)
        {
            print("0번 슬롯 스킬 입니다");
            SkillUse(skillSlot[0]); // 0번 슬롯일때 1번 어스 스킬 발동
            CoolTimeState(skillCoolTimeGauge, skillSlot[0], button[0]);
        }
        else if (slotNumber == 1)
        {
            print("1번 슬롯 스킬 입니다");
            SkillUse(skillSlot[1]); // 1번 슬롯일때 3번 윈드 스킬 발동
            CoolTimeState(skillCoolTimeGauge2, skillSlot[1], button[1]);
        }
        else
        {
            print("2번 슬롯 스킬 입니다");
            SkillUse(skillSlot[2]); // 2번 슬롯일때 5번 버프 스킬 발동
        }
    }

    void SkillUse(int skilNum)
    {
        switch (skilNum)
        {
            case 0:
                print("스킬이 존재하지 않습니다!!");
                break;
            case 1:
                print("어스 발동!!");
                OnEarthAttack();
                break;
            case 2:
                print("토네이도 발동!!");
                OnTornadoAttack();
                break;
            case 3:
                print("윈드 발동!!");
                OnWindAttack();
                break;
            case 4:
                print("메테오 발동!!");
                OnBigMeteorAttack();
                break;
            case 5:
                print("버프 발동!!");
                break;
            default:
                break;



        }
    }

    public void CoolTimeState(Image skillcoolTime, int skillNum, Button button)
    {
       
        float skillCool=0;
        button.enabled = false;
        if (skillNum == 1) skillCool = earth.coolTime;
        else if (skillNum == 2) skillCool = tornado.coolTime;
        else if (skillNum == 3) skillCool = wind.coolTime;
        else if (skillNum == 4) skillCool = meteor.coolTime;

        StartCoroutine(startSkillCoolTime(skillcoolTime, skillCool, button));
    }

    IEnumerator startSkillCoolTime(Image coolTimeimage, float coolTime, Button Getbutton)
    {   
        float subCoolTime = 1 / coolTime;
        
        int i = 0;
        coolTimeimage.fillAmount = 1;
        while(coolTimeimage.fillAmount>=0.001f)
        {

            coolTimeimage.fillAmount -= subCoolTime * Time.deltaTime;

            yield return new WaitForFixedUpdate();
            i++;
            if (i > 1000)
                break;
        }
        Getbutton.enabled = true;
       
    }

    public void OnClickchangedskill()
    {
        for (int i = 1; i < slot0skillimages.Length; i++)
        {
            slot0skillimages[i].SetActive(false);
        }
        slot0skillimages[skillSlot[0]].SetActive(true);


        for (int i = 1; i < slot1skillimages.Length; i++)
        {
            slot1skillimages[i].SetActive(false);
        }
        slot1skillimages[skillSlot[1]].SetActive(true);
        
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

        //isCoolTime = true;
       // skillCoolTime = this.earth.coolTime;
        //maxskillCool = this.earth.coolTime;

    

    }


    public void OnTornadoAttack()
    {
        if (isCoolTime) // 쿨타임일때 스킬버튼 눌러도 공격 안나감
        {
            return;
        }


        foreach (Vector2 direction in Tornadodir)
        {
            GameObject tornado = tornadoFactory.GetSkill();
            tornado.transform.position = playerTr.position;
            Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * tornadoSpeed;

        }

        isCoolTime = true;
        skillCoolTime = tornado.coolTime;
        maxskillCool = tornado.coolTime;

    }

    public void OnWindAttack()
    {
        

        for (int i = 0; i < Winddir.Length; i++)
        {
            GameObject wind = windFactory.GetSkill();
            wind.transform.position = playerTr.position;
            wind.transform.rotation = Quaternion.Euler(0, 0, angles[i]);
        }

        //isCoolTime = true;
        //skillCoolTime = wind.coolTime;
        //maxskillCool = wind.coolTime;
    }

    public void OnMeteorAttack()
    {
        if (isCoolTime) // 쿨타임일때 스킬버튼 눌러도 공격 안나감
        {
            return;
        }

        GameObject meteor = meteorFactory.GetSkill();
        float posX = Random.Range(-2, 13);
        meteor.transform.position = new Vector2(posX, 7);
        meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);

        isCoolTime = true;
        skillCoolTime = this.meteor.coolTime;
        maxskillCool = this.meteor.coolTime;
    }

    public void OnBigMeteorAttack()
    {
        if (isCoolTime) // 쿨타임일때 스킬버튼 눌러도 공격 안나감
        {
            return;
        }

        GameObject meteor = BigmeteorFactory.GetSkill();
        meteor.transform.position = new Vector2(7, 7);
        meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);

        isCoolTime = true;
        skillCoolTime = this.meteor.coolTime;
        maxskillCool = this.meteor.coolTime;
    }

    public void OnActivatedBuff()
    {
        BuffForthPrefab.SetActive(true);
        BuffBackPrefab.SetActive(true);
        StartCoroutine(DestroyBuff());
    }

    IEnumerator DestroyBuff()
    {
        yield return new WaitForSeconds(20.0f);
        BuffForthPrefab.SetActive(false);
        BuffBackPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
