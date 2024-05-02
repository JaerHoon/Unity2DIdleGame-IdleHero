using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{

    protected LayerMask layermask;
    protected int playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("player");
        layermask = 1 << playerLayer;

    }

    public void LayerCheck()
    {
        Collider2D recognitionPlayer = Physics2D.OverlapCircle(transform.position + Vector3.up * -0.1f, 10f, layermask, -100.0f, 100.0f);
        if (recognitionPlayer != null)
            print(recognitionPlayer.name);
    }

    // Update is called once per frame
    void Update()
    {
        LayerCheck();
    }
}
