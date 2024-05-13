using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
    [SerializeField]
    int TornadoDamage;
    [SerializeField]
    Player_ScriptableObject player;
    [SerializeField]
    Skill_ScriptableObject tornados;

    BoxCollider2D box;
    void Start()
    {   
        box = this.gameObject.GetComponent<BoxCollider2D>();
        Destroy(gameObject, 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("monster"))
        {
            TornadoDamage = player.playerDamage + StatusManager.instance.GetStatus(StatusManager.playerATkpow) + (int)tornados.damage;
            collision.gameObject.GetComponent<RecyclableMonster>().OnMonDamaged(TornadoDamage);
        }
    }
    public void OnTornadoColl()
    {
        box.enabled = true;
        Invoke("offTornadoColl", 0.5f);
    }

    void offTornadoColl()
    {
        box.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
        
    }
}
