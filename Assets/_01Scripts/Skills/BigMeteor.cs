using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    public GameObject ExPrefab;
    void Start()
    {
        
        StartCoroutine(meteorDestroy());
        
        
    }

    IEnumerator meteorDestroy()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
        MeteorEx();



    }

  
    void MeteorEx()
    {
        GameObject Ex = Instantiate(ExPrefab, this.gameObject.transform.position, Quaternion.identity);
        Destroy(Ex.gameObject, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 5.0f * Time.deltaTime);
        
    }
}
