using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ֱ�Ӹ�����ģ�ͣ�����һ�����壩���ؽű���Ҫ������unity�Դ������в��У���
public class Ex : MonoBehaviour
{
     public Move M;
    [SerializeField] private Animator Am;
    void Awake()
    {
         M = GetComponent<Move>();
    }

    void Update()
    {
        //Am.SetFloat("ForWard", M.NowUp);//ֻ����ǰ�����ܶ��������������ı߶��ܶ�

        Am.SetFloat("ForWard", M.MoveVecMag);
        // Rotate :
        //transform.forward���� �������ǰ������ǰ����Ȼ����Ϊ ��������
        //model.transform.forward = transform.right * M.NowRight + transform.forward * M.NowUp;
        if (M.MoveVecMag > 0.05f)
        {
           transform.forward = M.MoveVec;
        }
    }
}
