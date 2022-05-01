using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    bool attackLock = false;
    bool JumpLock = false;
    bool DefenceLock = false;
    Vector3 rootDeltaPos;
    [Header("try------------------------------------")]
    public float rollSpeed = 1.36f;

    float targerLerp = 1f;

    [Space(10)]
    //public float jabL = -0.1f;
    public float JumpSpeed = 12.39f;
     [Header("---------- GameObject相关 ---------")]
    private Vector3 Vtemp;
    private Vector3 YSet;

    private Vector3 Thrusvec;

    public GameObject model ;
    public Move M;
    public CmeraHandle CH;
    public Rigidbody RB;

    public float JabVelocity;
    public float AttackSpeed;

 
    [Space(14)]
    [Header("===========friction ============")]
    public PhysicMaterial f1;
    public PhysicMaterial f2;
    private CapsuleCollider col; 

    public Animator Am;
    void Awake()
    {
       Am = model.GetComponent<Animator>();
       M = GetComponent<Move>();
       RB = GetComponent<Rigidbody>();
       col = GetComponent<CapsuleCollider>();
       
    }

    void Update()
    {
        //Am.SetFloat("ForWard", M.NowUp);//只有往前它才能动，我想让它往哪边都能动

        Am.SetFloat("ForWard", M.MoveVecMag);
        
        if(M.IsAttack && check() && attackLock == false)
        {
             Am.SetTrigger("Attack");
        }
        
        if (RB.velocity.magnitude > 3.21f)
        {
            Am.SetTrigger("Roll");
        }

        if (M.IsJump == true &&  !JumpLock)
        { 
            Am.SetTrigger("Jump");

        }
        if(M.IsDefence == true && !DefenceLock)
        {
            Am.SetBool("Defense", true);
            
        }
        if (M.IsDefence == false  )
        {
            Am.SetBool("Defense", false);

        }

        if(M.IsLock)
        {
            CH.LockUnlock();
            Debug.Log(CH.targetObject);
        }
        // Rotate :
        //transform.forward代表 物体的正前方。正前方显然不能为 零向量。
        //model.transform.forward = transform.right * M.NowRight + transform.forward * M.NowUp;

        if (CH.LockState == false)
        {
            if (M.MoveVecMag > 0.05f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, M.MoveVec, 0.15f);

            }
        }
        else
        {
            model.transform.forward = transform.forward;
        }
       
        //:transform.position 是 瞬移， 没有进行物理变换，所以只能有碰撞，不能有持续
        //爬坡的状态。
        //if (Input.anyKey)
        //{
        //    model.transform.Translate(transform.forward * Time.deltaTime * 3.2f);
        //}
    }
    void FixedUpdate()
    {
        if (Am.GetCurrentAnimatorStateInfo(1).IsName("attack3"))
        { RB.position += rootDeltaPos; }

        if (Am.GetCurrentAnimatorStateInfo(0).IsName("roll"))
        { RB.position += rootDeltaPos * 0.28f; }

        
            RB.velocity = new Vector3(M.MoveVec.x, RB.velocity.y, M.MoveVec.z) + Thrusvec;
            Thrusvec = Vector3.Lerp(Thrusvec, Vector3.zero, 1f);
            //Thrusvec = Vector3.zero;
      
      
        rootDeltaPos = Vector3.zero;

    }
    bool check()
    {
        bool isGround = Am.GetCurrentAnimatorStateInfo(0).IsName("grond");

        if (isGround) return true;
        else return false;
    }
    bool check2()
    {
        if (!Am.GetCurrentAnimatorStateInfo(1).IsName("attack") && !Am.GetCurrentAnimatorStateInfo(1).IsName("attack2"))
            return true;
        else return false;
            }

    

    public void OnJump()
    {
        M.inputEnable = false;
        M.IsLockPlanar = true;
        Thrusvec = new Vector3(0, JumpSpeed, 0);
        attackLock = true;
        DefenceLock = true;

    }
    public void OnJab()
    {
        M.inputEnable = false;
        M.IsLockPlanar = true;
        attackLock = true;
        DefenceLock = true;
    }

    public void OnJabUpdate()
    {
        JabVelocity = Am.GetFloat("JabSpeed");

         Thrusvec = model.transform.forward * (JabVelocity);
    }
    //public void AfterJump()
    //{
    //   M.inputEnable = true;
    //   M.IsLockPlanar = false;
    //}
   
    public void IsOnGround()
    {
       Am.SetBool("OnGround",true);
    }
    public void IsNotOnGround()
    {
     
        Am.SetBool("OnGround", false);
    }
    public void IsOnRoll()
    {
        //RB.transform.position += M.MoveVec * 0.2f;

      //** Thrusvec = new Vector3(RB.velocity.x + rollSpeed, RB.velocity.y, RB.velocity.z); 
        M.inputEnable = false;
        M.IsLockPlanar = true;
        attackLock = true;
        DefenceLock = true;
    }

    public void OnGroundEnter()
    {
        col.material = f1;
         Thrusvec = new Vector3(rollSpeed, RB.velocity.y, RB.velocity.z); 

        M.inputEnable = true;
        M.IsLockPlanar = false;
        attackLock = false;
        JumpLock = false;
        DefenceLock = false;

    }
    public void OnAttackOnAttackRH1Enter()
    {
         M.inputEnable = false;
        DefenceLock = true;

        targerLerp = 1f; 
    }
    public void OnRHAttackUpdate()
    {
        AttackSpeed = Am.GetFloat("AttackVel");
        Thrusvec = model.transform.forward * (AttackSpeed) * 30f;

        float currentWeight = Am.GetLayerWeight(1);
        currentWeight = Mathf.Lerp(currentWeight, targerLerp, 0.06f);
        Am.SetLayerWeight(1, currentWeight);
    }

    public void OnAttack2()
    {
        DefenceLock = true;
        M.inputEnable = false; 
         JumpLock = true;
    }
    public void OnAttackIdle()
    {
        Am.SetLayerWeight(1, 1f);
        M.inputEnable = true;
        DefenceLock = false;

        JumpLock = false;
        targerLerp = 0f;
    }
   public void  idleUpdate()
    {
        float currentWeight = Am.GetLayerWeight(1);
        currentWeight = Mathf.Lerp(currentWeight, targerLerp, 0.02f);
        Am.SetLayerWeight(1, currentWeight);
    }
   public void OnDenfence()
    {
        M.inputEnable = false;
        JumpLock = true;
        attackLock = true;
    }
    public void OnDefenceIdle()
    {
        M.inputEnable = true;
        JumpLock = false;
        attackLock = false;
    }
    public void  OnDenfenceUpdate()
    {
        float currentWeight = Am.GetLayerWeight(2);
       // currentWeight = Mathf.Lerp(currentWeight, 1f, 1f);
        Am.SetLayerWeight(2, 1);
    }
    public  void OnDefenceIdleUpdate()
    {
        float currentWeight = Am.GetLayerWeight(2);
        currentWeight = Mathf.Lerp(currentWeight, 0f, 0.1f);
        Am.SetLayerWeight(2, currentWeight);
    }
    public void OnGroundExit()
    {
        col.material = f2;
    }
    public void RootMove(object deltaPos)
    {
        rootDeltaPos += (Vector3)deltaPos;
    }
}
