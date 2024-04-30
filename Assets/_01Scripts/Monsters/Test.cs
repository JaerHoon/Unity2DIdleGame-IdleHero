using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float posY = Utility.EaseInBounce(transform.position.y, transform.position.y - 20.0f, 5.0f * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float posY = Utility.EaseInBounce(transform.position.y,0.0f, 1f);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
