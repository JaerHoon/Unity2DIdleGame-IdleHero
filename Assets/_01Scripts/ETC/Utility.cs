using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    //중복없는 정수 난수 생성
    public static int[] RandomCreate(int count, int RangeMin, int RangeMax)
    {
        int[] nums = new int[count];
        //1. 매게변수에서 받은 count 개수와 같은 범위를 가진 배열을 만듭니다.

        List<int> rangenums = new List<int>();
        // 2. 그리고  또 다른 리스트를 하나 만듭니다. 

        for (int i = 0; i < RangeMax - RangeMin; i++)
        {
            rangenums.Add(i + RangeMin);// rangenums[0] = 0+RangeMin 이라는 숫자가 들어갑니다.
        }
        //3. 위에서 만든 리스트에 RangeMin부터 RangeMax까지의 정수를 추가합니다.

        for (int i = 0; i < count; i++)
        {
            int RandomNum = Random.Range(0, rangenums.Count);
            // 4.RandomNum 변수에 지정한 범위 내에서 랜덤한 수를 하나 추출합니다.
            nums[i] = rangenums[RandomNum - RangeMin];
            // 5. 그리고 rangenums리스트의 추출된 수에서 범위의 최소값을 뺀 순번째의 수를 처음 만든 배열 nums에 추가합니다.
            rangenums.RemoveAt(RandomNum - RangeMin);
            // 6. 그리고 한번 들어간 수는 rangenums 리스트에서 제거 됩니다. 그러므로 한번 나온 수가 추출되는 일은 없어집니다.

        }
        // 7.그렇게 난수 추출 개수 만큼 nums[] 배열에 담아줍니다.

        return nums; // 8.nums 라는 배열을 반환합니다.  

    }
}
