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

   
    
    [SerializeField]
    GameObject[] slot0skillimages;
    [SerializeField]
    GameObject[] slot1skillimages;

    PlayerMoving playermove;
    public Button button1; // 인스펙터에 스킬 버튼 넣기 위한 변수
    public Button button2; // 인스펙터에 스킬 버튼 넣기 위한 변수
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
        skillSlot[0] = 2; // 0번 스킬 슬롯에 1번 스킬 발동
        skillSlot[1] = 4; // 1번 스킬 슬롯에 3번 스킬 발동
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

        playermove = GameObject.FindWithTag("Player").GetComponent<PlayerMoving>();
        
    }

    
    public void ChangeSlot(int[] skillNum)
    {
        skillSlot = skillNum;
        OnClickchangedskill();


    }
    

    public void OnclickSkill(int slotNumber)
    {
   
        if (slotNumber == 0)
        {
            print("0번 슬롯 스킬 입니다");
            SkillUse(skillSlot[0]); // 0번 슬롯일때 1번 어스 스킬 발동
            CoolTimeState(skillCoolTimeGauge, skillSlot[0], button[0]); // 쿨타임이 돌아갑니다(쿨타임 시각이미지, 0번 스킬슬롯, 0번 스킬버튼)
        }
        else if (slotNumber == 1)
        {
            print("1번 슬롯 스킬 입니다");
            SkillUse(skillSlot[1]); // 1번 슬롯일때 3번 윈드 스킬 발동
            CoolTimeState(skillCoolTimeGauge2, skillSlot[1], button[1]);// 쿨타임이 돌아갑니다(쿨타임 시각이미지, 1번 스킬슬롯, 1번 스킬버튼)
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
                OnEarthAttack(); // Earth 스킬 발동 함수
                break;
            case 2:
                print("토네이도 발동!!");
                OnTornadoAttack(); // Tornado 스킬 발동 함수
                break;
            case 3:
                print("윈드 발동!!"); 
                OnWindAttack(); // Wind 스킬 발동 함수
                break;
            case 4:
                print("메테오 발동!!");
                OnBigMeteorAttack(); // Meteor 스킬 발동 함수
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
        button.enabled = false; // 스킬 발동되었을때 버튼을 비활성화하여 쿨타임중일때 스킬 발동되지 않게 한다.
        if (skillNum == 1) skillCool = earth.coolTime; // 1번스킬 Earth의 쿨타임 설정
        else if (skillNum == 2) skillCool = tornado.coolTime; // 2번스킬 Tornado의 쿨타임 설정
        else if (skillNum == 3) skillCool = wind.coolTime; // 3번스킬 wind의 쿨타임 설정
        else if (skillNum == 4) skillCool = meteor.coolTime; // 4번스킬 meteor의 쿨타임 설정

        StartCoroutine(startSkillCoolTime(skillcoolTime, skillCool, button)); // 쿨타임 이미지의 fillAmount 효과를 위한 코루틴
    }

    IEnumerator startSkillCoolTime(Image coolTimeimage, float coolTime, Button Getbutton)
    {   
        float subCoolTime = 1 / coolTime; // fillAmount '1'값을 위에서 설정한 스킬의 쿨타임값 만큼 나눠준다.
        
        int i = 0; // while 오류 방지용 
        coolTimeimage.fillAmount = 1; // fillAmount 값 설정
        while(coolTimeimage.fillAmount>=0.001f)
        {

            coolTimeimage.fillAmount -= subCoolTime * Time.deltaTime;

            yield return new WaitForFixedUpdate();
            i++;
            if (i > 1000)
                break;
        }
        Getbutton.enabled = true; // 쿨타임이 끝났을 때 스킬을 다시 사용하기 위해서 버튼 활성화

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
        // 버프 아이콘 장착
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
        PlayerSound.instance.OnEarthSound();
        earth.transform.position = earthPos.position;
    }


    public void OnTornadoAttack()
    {
        
        foreach (Vector2 direction in Tornadodir)
        {
            GameObject tornado = tornadoFactory.GetSkill();
            PlayerSound.instance.OnTornadoSound();
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
            PlayerSound.instance.OnWindSound();
            wind.transform.position = playerTr.position;
            wind.transform.rotation = Quaternion.Euler(0, 0, angles[i]);
        }
    }

    public void OnBigMeteorAttack()
    {
        GameObject meteor = BigmeteorFactory.GetSkill();
        PlayerSound.instance.OnMeteorSound();
        meteor.transform.position = new Vector2(7, 7);
        meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);
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
        if(playermove.isButtonPressed==true && button1.enabled==true)
        {
            OnclickSkill(0);
            
        }
        else if(playermove.isButtonPressed == true && button2.enabled == true)
        {
            OnclickSkill(1);
        }
        
    }
}
