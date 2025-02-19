using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class StageManager : MonoBehaviour,IQuestChecker

{

    public static StageManager instance;//스폰 매니저 싱글톤 생성

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //=============선언=================
    public Button stageStartButton;
    public TextMeshProUGUI stageText;

    public GameObject stageRestartBt;
    public GameObject gameOverPanel;

    [SerializeField]
    StageScData StageData;

    [SerializeField]
    int buttonDamage;
    [SerializeField]
    GameObject stageClearPanel;

    Sequence textSequence;

    public Action<StageScData, int> StartWave;
    public Action StageClear;

    public int StageNum;

    PlayerDamaged playerhp;

    public Quest_ScriptableObject.QuestType questType { get; set; }

    private void Start()
    {
        stageText.text = "";
        gameOverPanel.SetActive(false);
        stageClearPanel.SetActive(false);
        GOPalpha = gameOverPanel.GetComponent<Image>().color;
        playerhp = GameObject.Find("Player").GetComponent<PlayerDamaged>();
        Invoke("OnStartWave", 1.5f);
    }


    public void OnStartWave()//버튼으로 스테이지 시작 이벤트 연결 
    {
        MainCanvas.instance.Cancel();
        SpawnManager.instance.RestoreNum = 0;//스테이지 초기화 시 죽은 몬스터 수 초기화
        SpawnManager.instance.allSpawnNum = 0;//스테이지 초기화 시 스폰 몬스터 수 초기화
        stageStartButton.interactable = false;
        ChangeStageText();//스테이지 텍스트 이동 및 변경
        StartCoroutine(DelayStageStart());//텍스트 이동 시간 확보

    }

    void ChangeStageText()
    {
        stageText.enabled = true;
        string stageTx = "STAGE 1-" + (StageNum + 1).ToString();
        stageText.text = stageTx;
        textSequence = DOTween.Sequence();
        stageText.transform.position = Vector3.zero;
        Color color = stageText.color;
        color.a = 0;
        stageText.color = color;
        StartCoroutine(DelayStageAlhpa());
        textSequence.Append(stageText.transform.DOScale(new Vector3(2, 2, 1), 1.5f));
        textSequence.Append(stageText.transform.DOMove(new Vector3(-0.2f, 3.7f, 0), 0.7f).SetEase(Ease.OutCubic));
        textSequence.Join(stageText.transform.DOScale(new Vector3(0.68f, 0.68f, 1), 0.7f));

    }

    public void OnPlayerDie()
    {
        MainCanvas.instance.Cancel();
        gameOverPanel.SetActive(true);
        stageRestartBt.SetActive(false);
        StartCoroutine(FadeOutGameOverPanel());

    }

    Color GOPalpha;

    IEnumerator FadeOutGameOverPanel()
    {
        
        while (GOPalpha.a <= 0.98)
        {
            
            GOPalpha.a = Mathf.Lerp(GOPalpha.a, 1, Time.deltaTime * 2);
            yield return new WaitForFixedUpdate();
            gameOverPanel.GetComponent<Image>().color = GOPalpha;
            if (GOPalpha.a > 0.9f && GOPalpha.a < 0.95f)
            {
                stageRestartBt.SetActive(true);
                yield break;
            }
        }
    }

    IEnumerator FadeInGameOverPanel()
    {
        while (GOPalpha.a >= 0.02)
        {
            GOPalpha.a = Mathf.Lerp(GOPalpha.a, 0, Time.deltaTime * 2);
            yield return new WaitForFixedUpdate();
            gameOverPanel.GetComponent<Image>().color = GOPalpha;
            if (GOPalpha.a <= 0.1f)
            {
                gameOverPanel.SetActive(false);
                yield break;
            }
        }
       
    }


    IEnumerator DelayStageAlhpa()
    {
        Color color = stageText.color;
        float i = 0;
        while (i < 1)
        {
            i += 0.01f;
            color.a = i;
            stageText.color = color;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator DelayStageStart()
    {
        yield return new WaitForSeconds(2.5f);
        StartWave?.Invoke(StageData, StageNum);//스테이지 시작 이벤트 발생
    }

    public void OnStageWin()//스테이지 클리어 이벤트 수신 함수
    {

        StartCoroutine(StageWin());
        playerhp.Init();
    }


    IEnumerator StageWin()
    {
        stageClearPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        stageClearPanel.SetActive(false);

        StageNum++;//현재 스테이지 상승
        if (StageNum > 5)
        {
            UpdateQuestInfo();
        }
        if (StageNum >= StageData.Stages.Length)//마지막 스테이지 클리어 시 처음 스테이지로 돌아옴
            StageNum = 0;
        OnStartWave();
    }


    public void OnStageMonsterClear()//스테이지 클리어 이벤트 수신 함수//리스타트 버튼 누름
    {
        StageClear?.Invoke();
        SpawnManager.instance.OnDestroyAllMonster();
        StartCoroutine(FadeInGameOverPanel());
        Invoke("OnStartWave", 3.0f);
        GameObject player = GameObject.Find("Player");
        player.transform.position = Vector3.zero;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        player.GetComponent<PlayerDamaged>().Init();
    }

    public void OnStageMonsterDamaged()//스테이지 위 모든 몬스터 피격 이벤트 수신 함수
    {
        SpawnManager.instance.OnDamagedAllMonster(buttonDamage);
    }

    public void OnStageMonsterAllDie()//스테이지 위 모든 몬스터 피격 이벤트 수신 함수
    {
        SpawnManager.instance.OnDamagedAllMonster(9999);
    }



    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1.5f;
    }

    public void UpdateQuestInfo()
    {
        QuestManager.instance.UpDateQuest(Quest_ScriptableObject.QuestType.StageClear);
    }
}
