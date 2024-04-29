using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy();
    }
}
