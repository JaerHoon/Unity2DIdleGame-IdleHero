using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Gold_Coin : MonoBehaviour
{
    const float upCoinTime = 0.6f;
    public Transform collectCoinPos;
    // Start is called before the first frame update
    void Start()
    {
        collectCoinPos = GameObject.Find("CoinCollect").GetComponent<Transform>();
        transform.DORotate(Vector3.up * 180, 0.5f).SetEase(Ease.OutCubic).SetLoops(-1, LoopType.Restart);
        //transform.DOMoveY(transform.position.y + 1.0f, 1.0f).SetEase(Ease.OutCubic);
        //Invoke("A", 1.0f);
    }

    private void OnEnable()//몬스터의 위치로 초기화
    {
        transform.GetChild(0).localPosition = new Vector2(0, -0.379f);
        transform.rotation = Quaternion.identity;
        //transform.DOMoveY(transform.position.y + 1.0f, 1.0f).SetEase(Ease.OutCubic);
        //Invoke("A", 1.0f);
        //Invoke("A", 1.0f);
    }

    public void SetTransform(Transform tr)//외부에서 코인 위치 설정
    {
        transform.position = tr.position;
    }
    void BounceMovement()//코인이 떨어지면 튕김
    {
        transform.DOMoveY(transform.position.y - 1.8f, 2.0f).SetEase(Ease.OutBounce);
        transform.GetChild(0).DOMoveY(transform.position.y - 2.0f, 2.0f).SetEase(Ease.OutBounce);
        Invoke("CollectMovement", 1.5f);
    }
    void CollectMovement()//코인마다 모이는 속도를 조정
    {
        float randIndex = Random.Range(1.2f, 1.5f);
        transform.DOMove(collectCoinPos.position, randIndex).SetEase(Ease.InQuart);
    }
    public void CoinDrop()//몬스터 사망 시 코인 위로 올라가기
    {
        
        transform.DOMoveY(transform.position.y + 1.0f, upCoinTime).SetEase(Ease.OutCubic);
        transform.GetChild(0).DOMoveY(transform.position.y - 1.0f, upCoinTime).SetEase(Ease.OutCubic);
        Invoke("BounceMovement", upCoinTime - 0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
