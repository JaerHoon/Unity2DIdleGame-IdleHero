using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public Transform playerTr;
    void Start()
    {
        
    }

    void BuffTr()
    {
        gameObject.transform.position = playerTr.position;
    }

    // Update is called once per frame
    void Update()
    {
        BuffTr();
    }
}
