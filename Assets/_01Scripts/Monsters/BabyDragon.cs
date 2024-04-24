using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDragon : MonsterRecycle
{
    // Start is called before the first frame update

    public Transform playerPositionTest;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookPlayer(playerPositionTest.position);
    }
}
