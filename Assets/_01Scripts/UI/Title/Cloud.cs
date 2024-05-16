using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{

    public Material cloud;

    private void Start()
    {
        cloud = gameObject.GetComponent<Image>().material;
    }

    void Update()
    {

        float newOffSetX = cloud.mainTextureOffset.x + -0.05f * Time.deltaTime;
        Vector2 newOffset = new Vector2(newOffSetX, 0);
        cloud.mainTextureOffset = newOffset;



    }
}
