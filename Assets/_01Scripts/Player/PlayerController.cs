using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    float axisValueHorizontal; 
    float axisValueVertical;

    float scaleX;
    float scaleY;
    float scaleZ;

    float X_Speed;
    float Y_Speed;

    public float Speed;
    public float HP;


    
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();

        

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
      

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerAnim();
        
        
    }

    void PlayerAnim()
    {
        if (axisValueVertical == 0)// y축 이동이 없을 때
        {
            if (axisValueHorizontal > 0.0f)
            {
                transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                anim.SetTrigger("walk");
            }
            else if (axisValueHorizontal < -0.0f)
            {
                transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
                anim.SetTrigger("walk");
            }
            else
                anim.SetTrigger("idle");
        }
        else
        {
            if(axisValueVertical > 0)
                anim.SetTrigger("walk");
            else if(axisValueVertical < 0)
                anim.SetTrigger("walk");
            else
                anim.SetTrigger("idle");
        }
        
    }


    
    void PlayerMove()
    {
        axisValueHorizontal = Input.GetAxis("Horizontal");
        axisValueVertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(axisValueHorizontal) > 0 && Mathf.Abs(axisValueVertical) > 0) // 대각 이동속도 감소
        {
            X_Speed = Speed * Time.deltaTime * axisValueHorizontal * 0.7f;
            Y_Speed = Speed * Time.deltaTime * axisValueVertical * 0.7f;
        }
        else
        {
            X_Speed = Speed * Time.deltaTime * axisValueHorizontal;
            Y_Speed = Speed * Time.deltaTime * axisValueVertical;
        }

        transform.Translate(X_Speed, Y_Speed, 0);
        
    }
}
