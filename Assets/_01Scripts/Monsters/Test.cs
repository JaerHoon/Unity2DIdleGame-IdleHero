using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init(false, true, LogBehaviour.Verbose).SetCapacity(200, 50);
        
    }

    public void UpShadow()
    {
        transform.DOMoveY(0, 1.0f).SetEase(Ease.InBounce);
        print("upShadow");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
