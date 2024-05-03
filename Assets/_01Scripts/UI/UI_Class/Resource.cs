using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Resource : MonoBehaviour
{
    public static Resource instance;//스폰 매니저 싱글톤 생성

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

    [SerializeField]
    Resource_ScriptableObject ResourceData;

    [SerializeField]
    TextMeshProUGUI coinText;
    [SerializeField]
    TextMeshProUGUI jemText;

    public int coinNum;
    public int jemNum;

    // Start is called before the first frame update

    void Start()
    {
        coinNum = ResourceData.coin;//리소스 정보 불러오기
        jemNum = ResourceData.jem;
        if (coinNum != 0)
            coinText.text = string.Format("{0:#,###}", coinNum);
        else
            coinText.text = "0";
        if (jemNum != 0)
            jemText.text = string.Format("{0:#,###}", jemNum);
        else
            jemText.text = "0";
    }

    private void OnDisable()
    {
        ResourceData.coin = coinNum;//게임 종료 시 현재 정보 저장
        ResourceData.jem = jemNum;

    }

    public void GetResource(int coinNum, int jemNum)
    {
        this.coinNum += coinNum;
        this.jemNum += jemNum;

        coinText.text = string.Format("{0:#,###}", this.coinNum);
        jemText.text = string.Format("{0:#,###}", this.jemNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
