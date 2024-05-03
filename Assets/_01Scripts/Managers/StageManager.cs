using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Button _1StartButton;
    public Button _2StartButton;
    public Button _3StartButton;
    public Button _4StartButton;

    [SerializeField]
    StageData currentStageData;
    [SerializeField]
    int buttonDamage;

    public StageData StageData1;
    public StageData StageData2;
    public StageData StageData3;
    public StageData StageData4;
    public StageData StageData5;
    public Action<StageData> StartWave;

    
    public int StageNum = 1;
    bool isStartStage = false;

    public void OnStartWave()//버튼으로 스테이지 시작 이벤트 연결 
    {

        stageStartButton.interactable = false;
        ButtonActivate(false);//모든 스체이지 버튼 비활성화
        if (StageNum == 1)
            StartWave?.Invoke(StageData1);//스테이지 시작 이벤트 발생
        else if(StageNum == 2)
            StartWave?.Invoke(StageData2);
        else if (StageNum == 3)
            StartWave?.Invoke(StageData3);
        else if (StageNum == 4)
            StartWave?.Invoke(StageData4);
        else
            StartWave?.Invoke(StageData5);
    
    }

    public void OnStageWin()//스테이지 클리어 이벤트 수신 함수
    {
        
        isStartStage = false;//버튼 활성화
        StageNum++;//현재 스테이지 상승
        currentStageData.currentStageNum = StageNum;//스테이지 정보 저장
       
    }

    public void OnStageMonsterClear()//스테이지 클리어 이벤트 수신 함수
    {
        SpawnManager.instance.OnDestroyAllMonster();
        stageStartButton.interactable = true;
        ButtonActivate(true);
    }

    public void OnStageMonsterDamaged()//스테이지 위 모든 몬스터 피격 이벤트 수신 함수
    {
        SpawnManager.instance.OnDamagedAllMonster(buttonDamage);
    }

    public void OnStageMonsterAllDie()//스테이지 위 모든 몬스터 피격 이벤트 수신 함수
    {
        SpawnManager.instance.OnDamagedAllMonster(9999);
    }

    public void OnChangeStage(int num)
    {
        if(num == 1)
        {
            StageNum = 1;
            ButtonActivate(true);
            _1StartButton.interactable = false;
        }
        else if(num == 2)
        {
            StageNum = 2;
            ButtonActivate(true);
            _2StartButton.interactable = false;
        }
        else if (num == 3)
        {
            StageNum = 3;
            ButtonActivate(true);
            _3StartButton.interactable = false;
        }
        else if (num == 4)
        {
            StageNum = 4;
            ButtonActivate(true);
            _4StartButton.interactable = false;
        }
    }
    void ButtonActivate(bool TF)
    {
        _1StartButton.interactable = TF;
        _2StartButton.interactable = TF;
        _3StartButton.interactable = TF;
        _4StartButton.interactable = TF;
    }
    // Start is called before the first frame update
    void Start()
    {
        StageNum = currentStageData.currentStageNum;//현재 스테이지 정보를 불러오기
        _1StartButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
