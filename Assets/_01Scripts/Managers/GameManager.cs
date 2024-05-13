using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;//일시정지 패널 프리팹
    private bool isPause = false;
    public bool pressStartButton;

    private static GameManager _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
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

    public void ChangeScene()
    {
        pressStartButton = true;
        LoadingSceneManager.LoadScene(2);
    }
    
}
