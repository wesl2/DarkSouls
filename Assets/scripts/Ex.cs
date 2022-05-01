using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//不能直接给建的模型（不算一个物体）挂载脚本，要让其在unity自带物体中才行？！
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
        //Am.SetFloat("ForWard", M.NowUp);//只有往前它才能动，我想让它往哪边都能动

        Am.SetFloat("ForWard", M.MoveVecMag);
        // Rotate :
        //transform.forward代表 物体的正前方。正前方显然不能为 零向量。
        //model.transform.forward = transform.right * M.NowRight + transform.forward * M.NowUp;
        if (M.MoveVecMag > 0.05f)
        {
           transform.forward = M.MoveVec;
        }
    }
}
