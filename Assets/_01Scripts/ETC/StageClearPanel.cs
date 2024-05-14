using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearPanel : MonoBehaviour
{
    [SerializeField]
    GameObject stageClear;
    [SerializeField]
    GameObject effect;

    

    private void OnEnable()
    {
        if(stageClear==null) stageClear = transform.GetChild(0).gameObject;
        if(effect == null) effect = transform.GetChild(1).gameObject;

        stageClear.SetActive(false);
        effect.SetActive(true);
        Invoke("Change", 0.8f);
    }

    private void Change()
    {
        stageClear.SetActive(true);
        effect.SetActive(false);
    }
}
