using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField]
    int EarthDamage;
    [SerializeField]
    Player_ScriptableObject player;
    [SerializeField]
    Skill_ScriptableObject earth;
    
    BoxCollider2D box;

    void Start()
    {
        box = this.gameObject.GetComponent<BoxCollider2D>();
        box.enabled = false;
        Destroy(gameObject, 0.5f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("monster"))
        {
            EarthDamage = player.playerDamage + StatusManager.instance.GetStatus(StatusManager.playerATkpow) + (int)earth.damage;
            collision.gameObject.GetComponent<RecyclableMonster>().OnMonDamaged(EarthDamage);
        }
    }

    public void OnEarthColl()
    {
       box.enabled = true;
        Invoke("offEarthColl", 0.1f);
    }

    void offEarthColl()
    {
        box.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
    
    }
}
