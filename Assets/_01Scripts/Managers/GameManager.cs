using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;//일시정지 패널 프리팹
    private bool isPause = false;

    public static GameManager instance;//스폰 매니저 싱글톤 생성

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

    //public PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        //playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }

    public void OnSetPausePanel()
    {
        isPause = !isPause;
        pausePanel.SetActive(isPause);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
