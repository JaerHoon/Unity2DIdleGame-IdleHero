using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{

    public float blinkSpeed = 1.0f; // 깜빡이는 속도 조절을 위한 변수

    private TextMeshProUGUI textMesh;
    

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        // 코루틴을 이용하여 깜빡이는 효과를 주기 위해 Start()에서 코루틴을 시작합니다.
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        int A = 0;
        while (true)
        {
            
            A++;
            print(A);
            if (A == 1000) StopAllCoroutines();
            // 알파 값을 조절하여 텍스트를 깜빡이게 합니다.
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, Mathf.PingPong(Time.time * blinkSpeed, 1.0f));
            yield return null;
        }

       
    }



}
