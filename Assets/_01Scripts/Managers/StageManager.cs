using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class StageManager : MonoBehaviour
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


    [SerializeField]
    StageScData StageData;

    [SerializeField]
    int buttonDamage;

    Sequence textSequence;

    public Action<StageScData, int> StartWave;



    

    bool isStartStage = false;

    public void OnStartWave()//버튼으로 스테이지 시작 이벤트 연결 
    {

        SpawnManager.instance.RestoreNum = 0;//스테이지 초기화 시 죽은 몬스터 수 초기화
        SpawnManager.instance.allSpawnNum = 0;//스테이지 초기화 시 스폰 몬스터 수 초기화
        stageStartButton.interactable = false;
        ChangeStageText();//스테이지 텍스트 이동 및 변경
        StartCoroutine(DelayStageStart());//텍스트 이동 시간 확보

    }

    void ChangeStageText()
    {
        string stageTx = "Stage 1-" + (StageData.currentStageNum + 1).ToString();
        stageText.text = stageTx;
        textSequence = DOTween.Sequence();
        stageText.transform.position = Vector3.zero;
        Color color = stageText.color;
        color.a = 0;
        stageText.color = color;
        StartCoroutine(DelayStageAlhpa());
        textSequence.Append(stageText.transform.DOScale(new Vector3(2, 2, 1), 1.5f));
        textSequence.Append(stageText.transform.DOMove(new Vector3(1.8f, 4.2f, 0), 0.7f).SetEase(Ease.OutCubic));
        textSequence.Join(stageText.transform.DOScale(new Vector3(1, 1, 1), 0.7f));

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
        StartWave?.Invoke(StageData, StageData.currentStageNum);//스테이지 시작 이벤트 발생
    }

    public void OnStageWin()//스테이지 클리어 이벤트 수신 함수
    {

        StageData.currentStageNum++;//현재 스테이지 상승
        if (StageData.currentStageNum >= StageData.Stages.Length)//마지막 스테이지 클리어 시 처음 스테이지로 돌아옴
            StageData.currentStageNum = 0;
        OnStartWave();

    }

    public void OnStageMonsterClear()//스테이지 클리어 이벤트 수신 함수
    {
        SpawnManager.instance.OnDestroyAllMonster();
        OnStartWave();
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
        
    }
}
