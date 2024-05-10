using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpPos : MonoBehaviour
{
    [SerializeField]
    Transform playerTr;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerTr.position + Vector3.up;
    }
}
