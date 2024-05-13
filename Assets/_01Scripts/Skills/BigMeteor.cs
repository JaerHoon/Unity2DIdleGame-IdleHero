using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    public GameObject ExPrefab;
    [SerializeField]
    int meteorDamage;
    [SerializeField]
    Player_ScriptableObject player;
    [SerializeField]
    Skill_ScriptableObject meteor;
    void Start()
    {
        meteorDamage = player.playerDamage + StatusManager.instance.GetStatus(StatusManager.playerATkpow) + (int)meteor.damage;
        StartCoroutine(meteorDestroy());
    }
    IEnumerator meteorDestroy()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
        MeteorEx();



    }

    public int MeteorDamage()
    {
        int meteorDam = 0;

        meteorDam = player.playerDamage + StatusManager.instance.GetStatus(StatusManager.playerATkpow) + (int)meteor.damage;

        return meteorDam;
    }
  
    void MeteorEx()
    {
        SpawnManager.instance.OnDamagedAllMonster(MeteorDamage());
        GameObject Ex = Instantiate(ExPrefab, this.gameObject.transform.position, Quaternion.identity);
        Destroy(Ex.gameObject, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 5.0f * Time.deltaTime);
        
    }
}
