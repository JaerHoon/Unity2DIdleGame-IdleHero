using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHunt : MonoBehaviour
{
    public float rotationSpeed = 50f; // 회전 속도
    PlayerMoving player;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMoving>();
    }


    void Update()
    {

        if(player.isButtonPressed==true)
        {
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        }
        
        
       
    }

    private void LateUpdate()
    {
        foreach(Transform child in transform)
        {
            child.rotation = Quaternion.Euler(0f, 0f, 10f);
        }
    }
}
