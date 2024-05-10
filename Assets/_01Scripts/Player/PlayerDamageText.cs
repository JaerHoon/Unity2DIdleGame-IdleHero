using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerDamageText : MonoBehaviour
{
    [SerializeField]
    float moveSpeed; // 텍스트 이동속도
    [SerializeField]
    float alphaSpeed; // 투명도 변환속도
    [SerializeField]
    float destroyTime;
    

    public int Damage;

    TextMeshPro text;
    Color alpha;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = Damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
