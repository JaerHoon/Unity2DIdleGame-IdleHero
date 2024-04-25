using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory
{//몬스터 재활용 스크립트

    List<RecyclableMonster> pool = new List<RecyclableMonster>();//몬스터 풀 리스트 저장
    RecyclableMonster monsterPrefab;//몬스터 프리팹
    int defaultMonsterNumber;//처음 pool에 만들 몬스터 수

    //프리팹 정보 강제 주입
    public MonsterFactory(RecyclableMonster monsterPrefab, int defaultMonsterNumber = 5)
    {
        this.monsterPrefab = monsterPrefab;//외부에서 받은 프리팹 정보 
        this.defaultMonsterNumber = defaultMonsterNumber;//외부에서 받은 몬스터 수 설정
        Debug.Assert(this.monsterPrefab != null, "몬스터 팩토리에 몬스터 프리팹 없음");
    }


    //오브젝트 생성
    void CreatePool()
    {
        for (int i = 0; i < defaultMonsterNumber; i++)
        {//비활성으로 생성할 몬스터 생성
            RecyclableMonster obj = GameObject.Instantiate(monsterPrefab) as RecyclableMonster;
            obj.gameObject.SetActive(false);//생성 후 바로 비활성화
            pool.Add(obj);//pool 리스트에 저장
        }
    }

    //몬스터 불러오기
    public RecyclableMonster GetMonster()
    {
        if(pool.Count == 0) CreatePool();//pool에 남아있는 몬스터가 없다면 몬스터 생성
        int lastIndex = pool.Count - 1;// pool리스트의 마지막 인덱스
        RecyclableMonster obj = pool[lastIndex];//pool리스트 마지막 몬스터를 obj에 담음
        pool.RemoveAt(lastIndex);//비활성 상태의 리스트 마지막 몬스터를 리스트에서 제거(사용 했으면 리스트에서 제거하기 위해)
        obj.gameObject.SetActive(true);//사용을 위해 몬스터 활성화
        return obj;//몬스터 
 

       
    }

    //반납함수 재활용하기 위해
    public void MonsterRestore(RecyclableMonster obj)
    {
        Debug.Assert(obj != null, "아무것도 없는 오브젝트는 반환되어야 합니다");
        obj.gameObject.SetActive(false);//상용된 몬스터 비활성화
        pool.Add(obj);//재활용하기 위해 다시 pool에 추가
    }
}
