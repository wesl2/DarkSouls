using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
   Vector3 P1,P2;
    float radius;
    public GameObject Model;
    public  CapsuleCollider capol;

    //��������³�����P1��P2���»����ϣ��³������׼�⡣
    public float offset = 0.1f;
    void Awake()
    {
        ////ע�⣺Ҫ���½��ҵ�λ�ã���Ȼ��Զ����һ��ʼ��λ���ˡ���
        radius = capol.radius;
        //P1 = transform.position + transform.up * radius;
        //P2 = transform.position + transform.up * capol.height - radius*transform.up;
        
    }

    // Update is called once per frame
    void Update()
    {
        P1 = transform.position -new Vector3(0,0.12f,0)+ transform.up * radius;
        P2 = transform.position + transform.up * capol.height - radius*transform.up;
        //���︵��ʦ�õ������խ��������ģ�ͣ�����ֱ����������׼�⣬�õĸ���
        //��������һ��Ҫ����������������������Ĳ��ˡ�����
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
