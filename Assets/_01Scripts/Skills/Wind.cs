using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Wind : MonoBehaviour
{
    public Ease ease;
    void Start()
    {
        transform.DOScale(5.0f, 1.5f).SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * 5.0f * Time.deltaTime);
        Destroy(gameObject, 1.5f);
    }
}
