using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void OnPlayerDamaged(int monDamage)
    {
        print("플레이어가 공격 받았습니다!! 데미지 : "+ monDamage);
    }
    
    void Update()
    {
        
    }
}
