using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;//胶迄 概聪历 教臂沛 积己

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

    public PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
