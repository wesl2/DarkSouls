using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
   Vector3 P1,P2;
    float radius;
    public GameObject Model;
    public  CapsuleCollider capol;

    //这个用于下沉，让P1、P2向下或向上，下沉更容易监测。
    public float offset = 0.1f;
    void Awake()
    {
        ////注意：要更新胶囊的位置，不然永远都是一开始的位置了。。
        radius = capol.radius;
        //P1 = transform.position + transform.up * radius;
        //P2 = transform.position + transform.up * capol.height - radius*transform.up;
        
    }

    // Update is called once per frame
    void Update()
    {
        P1 = transform.position -new Vector3(0,0.12f,0)+ transform.up * radius;
        P2 = transform.position + transform.up * capol.height - radius*transform.up;
        //这里傅老师用的让其变窄，更贴合模型，我是直接让其更容易检测，用的更大。
        //处理数据一定要声明变量，否则后续根本改不了。。。
        Collider[] output = Physics.OverlapCapsule(P1, P2, radius-0.06f, LayerMask.GetMask("Ground"));
        if (output.Length > 0)
        {
            SendMessageUpwards("IsOnGround");
        }
        else
        {
            SendMessageUpwards("IsNotOnGround"); 
        }
    }
}
