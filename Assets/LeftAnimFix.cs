using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAnimFix : MonoBehaviour
{
    Animator Am;
    public Vector3 BoneEuler;
    Vector3 TempV;
    public float EulerToLeftLowerArm;
    private void Awake()
    {
        Am = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(!Am.GetCurrentAnimatorStateInfo(2).IsName("defense"))
        {
            Transform tr = Am.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            tr.localEulerAngles += BoneEuler;
            Am.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(tr.localEulerAngles));
        }

    }
}
