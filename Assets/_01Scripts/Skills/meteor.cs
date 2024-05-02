using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class meteor : MonoBehaviour
{
    //public Ease ease;
    Animator anim;
    void Start()
    {
        //transform.DOScale(5.0f, 1.5f).SetEase(ease);
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "monster")
        {
            anim.SetTrigger("explosion");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 2.0f * Time.deltaTime);
    }
}
