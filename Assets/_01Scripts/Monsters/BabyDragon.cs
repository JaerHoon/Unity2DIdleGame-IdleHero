using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BabyDragon : RecyclableMonster
{
    // Start is called before the first frame update

    public Transform playerPositionTest;
    [SerializeField]
    MonsterData drangonData;

    [SerializeField]
    bool isAtk = false;

    void Start()
    {
        
    }


    

    // Update is called once per frame
    void Update()
    {
        isAtk = isAttacking;
        LookPlayer(playerPositionTest.position);
        MonsterState(playerPositionTest.position, drangonData.attackDistance ,drangonData.attackSpeed);
        UpdateState(playerPositionTest.position, drangonData.moveSpeed);
    }
}
