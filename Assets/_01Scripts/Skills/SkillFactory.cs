using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    //코인 재활용 스크립트

    List<GameObject> pool = new List<GameObject>();//스킬 풀 리스트 저장
    GameObject skillPrefab;//스킬 프리팹
    int defaultCoinNumber;//처음 pool에 만들 스킬 갯수

    //프리팹 정보 강제 주입
    public SkillFactory(GameObject skillPrefab, int defaultCoinNumber = 4)
    {
        this.skillPrefab = skillPrefab;//외부에서 받은 프리팹 정보 
        this.defaultCoinNumber = defaultCoinNumber;//외부에서 받은 스킬 갯수 설정
        Debug.Assert(this.skillPrefab != null, "몬스터 팩토리에 코인 프리팹 없음");
    }


    //오브젝트 생성
    void CreatePool()
    {
        for (int i = 0; i < defaultCoinNumber; i++)
        {//비활성으로 생성할 스킬 생성
            GameObject obj = GameObject.Instantiate(skillPrefab) as GameObject;
            obj.gameObject.SetActive(false);//생성 후 바로 비활성화
            pool.Add(obj);//pool 리스트에 저장
        }
    }

    //몬스터 불러오기
    public GameObject GetSkill()
    {
        if (pool.Count == 0) CreatePool();//pool에 남아있는 몬스터가 없다면 코인 생성
        int lastIndex = pool.Count - 1;// pool리스트의 마지막 인덱스
        GameObject obj = pool[lastIndex];//pool리스트 마지막 스킬을 obj에 담음
        pool.RemoveAt(lastIndex);//비활성 상태의 리스트 마지막 스킬 리스트에서 제거(사용 했으면 리스트에서 제거하기 위해)
        obj.gameObject.SetActive(true);//사용을 위해 스킬 활성화
        return obj;//코인 



    }

    //반납함수 재활용하기 위해
    public void CoinRestore(GameObject obj)
    {
        Debug.Assert(obj != null, "아무것도 없는 오브젝트는 반환되어야 합니다");
        obj.gameObject.SetActive(false);//상용된 스킬 비활성화
        pool.Add(obj);//재활용하기 위해 다시 pool에 추가
    }
}
