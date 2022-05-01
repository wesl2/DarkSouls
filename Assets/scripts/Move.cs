using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("-------- �����ƶ�����--------")]
    public string KeyUp = "w";
    public string KeyDown = "s";
    public string KeyRight = "d";
    public string KeyLeft = "a";

    public string KeyJUp = "up";
    public string KeyJDown = "down";
    public string KeyJRight = "right";
    public string KeyJLeft = "left";

    public string KeyLock;
    public string KeyRHAttack;
    public string KeyRun;
    public string KeyJump;
    public string KeyDefence;
    [Header("-------- �ٶȵ��� --------")]


    public float MoveSpeed;
    public float RunSpeed;
    public float WalkSpeed;


    [Header("-------- �ű������ֵ�ź� --------")]
    public float SetTime = 0.1f;

    public float Watch;

    public Vector3 MoveVec;
    public float MoveVecMag;

    public float NowUp;
    public float NowRight;

    public bool IsRun = false;
    public bool IsJump = false;
    public bool IsAttack = false;
    public bool IsDefence = false;
    //*//public bool IsNotDefence = false;
    public bool IsLock = false;

    public bool inputEnable = true;
    public bool IsLockPlanar = false;
    public bool AttackTomoreAttack = false;


    private float targetUp;
    private float targetRight;
    private float a1, a2,t;
    
    public float JUp;
    public float JRight;
    

    private bool RunTemp;

    public MyButton ButtonA = new MyButton();
    public MyButton ButtonB = new MyButton();
    public MyButton ButtonC = new MyButton();
    public MyButton ButtonD = new MyButton();
    public MyButton ButtonE = new MyButton();
    public MyButton ButtonF = new MyButton();
    public MyButton ButtonG = new MyButton();

    void Update()
    {
        ButtonA.Tick(Input.GetKey(KeyRun));
        ButtonB.Tick(Input.GetKey(KeyJump));
        ButtonC.Tick(Input.GetKey(KeyDefence));
        ButtonD.Tick(Input.GetKey(KeyRHAttack));
        ButtonE.Tick(Input.GetKey(KeyLock));
        
        //ButtonE.Tick(Input.GetKey(KeyRun));
        //ButtonF.Tick(Input.GetKey(KeyRun));


    targetUp = (Input.GetKey(KeyUp) ? 1.0f : 0f) - (Input.GetKey(KeyDown) ? 1.0f: 0f);
    targetRight = (Input.GetKey(KeyRight) ? 1.0f : 0f) - (Input.GetKey(KeyLeft) ? 1.0f: 0f);

     JUp = (Input.GetKey(KeyJUp) ? 1.0f : 0f) - (Input.GetKey(KeyJDown) ? 1.0f : 0f);
     JRight = (Input.GetKey(KeyJRight) ? 1.0f : 0f) - (Input.GetKey(KeyJLeft) ? 1.0f : 0f);


        //*//IsDefence = Input.GetKey(KeyDefence);
        //*//IsNotDefence = Input.GetKeyDown(KeyDefence);
        //*//IsAttack = Input.GetKeyDown(KeyRHAttack);
        //*//IsRun = Input.GetKey(KeyRun);        
        //*//IsJump = Input.GetKey(KeyJump);

        IsLock = ButtonE.onPressed;
        IsDefence = ButtonC.isPressing ;
        IsAttack = ButtonD.onPressed  ;
        IsRun = (ButtonA.isPressing && !ButtonA.isDelaying)&&(!ButtonC.isPressing);
        IsJump = ButtonB.onPressed;
        //ע������ֵ��˳�� 0Ҫ��ƽ��֮ǰ�������û��������
        if (inputEnable == false )
        {
            targetUp = 0;
            targetRight = 0;
            
            //���ʣ���ô�����ܲ�����أ����Ӱ�쵽������ǰ���ٶȡ�
            //���ǽ���ϡ�����controller�ű���shiftû�б��̶�ס��
            //��ô��ֱ���ڵ�ǰ����ű�����֮���С�
            //IsAttack = false;
            //IsJump = false;
        }
       
        //Be careful about GetKey and GetKeyDown!!

        NowUp = Mathf.SmoothDamp(NowUp,targetUp,ref a1,SetTime);
     NowRight = Mathf.SmoothDamp(NowRight, targetRight, ref a2, SetTime);

        //if(inputEnable == false)
        //{
        //    targetUp = 0;
        //    targetRight = 0;
        //}
        
        
        //��Ŀ��������if

        float MoveMagTemp = Mathf.Sqrt(NowRight * NowRight + NowUp * NowUp) * (IsRun ? 2f : 1f);
      //float MoveMagTemp = Mathf.Max(Mathf.Abs(NowRight), Mathf.Abs(NowUp)) * (IsRun ? 2f : 1f);
        //����Ч��
        MoveVecMag = Mathf.Lerp(MoveVecMag, MoveMagTemp, 0.1f);
        //Watch = MoveVecMag;
        //ע�⣺vector3������ģ�transform���Լ���
        // MoveVec = NowUp * Vector3.forward + NowRight * Vector3.right;
        if (IsLockPlanar == false)
        {
            if (MoveVecMag > 1.02f)
            {
                MoveVec = (NowUp * transform.forward + NowRight * transform.right).normalized * (IsRun ? RunSpeed : WalkSpeed);
            }
            else
                MoveVec = (NowUp * transform.forward + NowRight * transform.right) * (IsRun ? RunSpeed : WalkSpeed);
        }

    }
     
}
