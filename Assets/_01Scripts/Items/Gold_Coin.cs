using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Gold_Coin : MonoBehaviour
{
    const float upCoinTime = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.DOMoveY(transform.position.y + 1.0f, 1.0f).SetEase(Ease.OutCubic);
        //Invoke("A", 1.0f);
    }

    private void OnEnable()
    {
        //transform.DOMoveY(transform.position.y + 1.0f, 1.0f).SetEase(Ease.OutCubic);
        //Invoke("A", 1.0f);
        //Invoke("A", 1.0f);
    }

    public void SetTransform(Transform tr)
    {
        transform.position = tr.position;
    }
    void BounceMovement()
    {
        transform.DOMoveY(transform.position.y - 2.0f, 2.0f).SetEase(Ease.OutBounce);
    }
    public void CoinDrop()
    {
        transform.DOMoveY(transform.position.y + 1.0f, upCoinTime).SetEase(Ease.OutCubic);
        Invoke("BounceMovement", upCoinTime - 0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
