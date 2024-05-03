using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFactory
{
    //코인 재활용 스크립트

    List<Gold_Coin> pool = new List<Gold_Coin>();//몬스터 풀 리스트 저장
    Gold_Coin coinPrefab;//코인 프리팹
    int defaultCoinNumber;//처음 pool에 만들 몬스터 수

    //프리팹 정보 강제 주입
    public CoinFactory(Gold_Coin coinPrefab, int defaultCoinNumber = 5)
    {
        this.coinPrefab = coinPrefab;//외부에서 받은 프리팹 정보 
        this.defaultCoinNumber = defaultCoinNumber;//외부에서 받은 코인 수 설정
        Debug.Assert(this.coinPrefab != null, "몬스터 팩토리에 코인 프리팹 없음");
    }


    //오브젝트 생성
    void CreatePool()
    {
        for (int i = 0; i < defaultCoinNumber; i++)
        {//비활성으로 생성할 코인 생성
            Gold_Coin obj = Gold_Coin.Instantiate(coinPrefab) as Gold_Coin;
            obj.gameObject.SetActive(false);//생성 후 바로 비활성화
            pool.Add(obj);//pool 리스트에 저장
        }
    }

    //몬스터 불러오기
    public Gold_Coin GetCoin()
    {
        if (pool.Count == 0) CreatePool();//pool에 남아있는 몬스터가 없다면 코인 생성
        int lastIndex = pool.Count - 1;// pool리스트의 마지막 인덱스
        Gold_Coin obj = pool[lastIndex];//pool리스트 마지막 몬스터를 obj에 담음
        pool.RemoveAt(lastIndex);//비활성 상태의 리스트 마지막 코인 리스트에서 제거(사용 했으면 리스트에서 제거하기 위해)
        obj.gameObject.SetActive(true);//사용을 위해 코인 활성화
        return obj;//코인 



    }

    //반납함수 재활용하기 위해
    public void CoinRestore(Gold_Coin obj)
    {
        Debug.Assert(obj != null, "아무것도 없는 오브젝트는 반환되어야 합니다");
        obj.gameObject.SetActive(false);//상용된 코인 비활성화
        pool.Add(obj);//재활용하기 위해 다시 pool에 추가
    }
}
