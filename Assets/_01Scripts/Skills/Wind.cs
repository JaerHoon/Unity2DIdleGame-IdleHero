using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Wind : MonoBehaviour
{
    public Ease ease;
    [SerializeField]
    int WindDamage;
    [SerializeField]
    Player_ScriptableObject player;
    [SerializeField]
    Skill_ScriptableObject wind;
    
    BoxCollider2D box;
    void Start()
    {
        transform.DOScale(5.0f, 1.5f).SetEase(ease);
        box = this.gameObject.GetComponent<BoxCollider2D>();
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("monster"))
        {
            WindDamage = player.playerDamage + StatusManager.instance.GetStatus(StatusManager.playerATkpow) + (int)wind.damage;
            collision.gameObject.GetComponent<RecyclableMonster>().OnMonDamaged(WindDamage);
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * 5.0f * Time.deltaTime);
       
    }
}
