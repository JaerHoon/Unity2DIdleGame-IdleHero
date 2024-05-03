using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
   
    void Start()
    {
        
    }
    public void Destroy()
    {
        Destroy(gameObject, 1.0f);
    }

    
    // Update is called once per frame
    void Update()
    {
        Destroy();
        
    }
}
