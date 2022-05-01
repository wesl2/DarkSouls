using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CmeraHandle : MonoBehaviour
{
    public float HorizontalSpeed = 50.336f;
    public float VerticalSpeed = 45f;
    public Move Mo;
    GameObject PlayerHandle;
   public GameObject Camera_Handle;//不要和脚本名一样！！！！！！
     public GameObject Main_Camera;
    public Image Lockdot;
    public bool LockState = false;

    public GameObject model;
    public GameObject targetObject;
    public CapsuleCollider cap;
    public BoxCollider box;
     

    float TempEulerFloat = 0;
      void Awake()
    {
        Camera_Handle = transform.parent.gameObject;
        PlayerHandle = Camera_Handle.transform.parent.gameObject;
        Main_Camera = Camera.main.gameObject;
        Lockdot.enabled = false;
     }

    void FixedUpdate()
    {
        //注意：我这里写的是让整个Handle的gameObject不动，
        //实际上可以让model不懂，而Handle正常动，这样看上去人物就没有随着
        //左右而改变前方了。获取model的办法是看脚本里哪里有，就可以直接接收。
        //当然也可以直接灌进去。
        //Vector3 tempEuler = PlayerHandle.transform.eulerAngles;
        //下列代码报错：这里的transform.rotation 本质上是个四元数。
        //Vector3显然不能直接获取它，要转换成欧拉角或者直接用欧拉角。
        //Vector3 tempEuler = model.transform.rotation ;
        if (targetObject == null)
        {
            Vector3 tempEuler = model.transform.eulerAngles;//类似死锁

            PlayerHandle.transform.Rotate(Vector3.up, Mo.JRight * Time.fixedDeltaTime * HorizontalSpeed);

            TempEulerFloat -= Mo.JUp * VerticalSpeed * Time.fixedDeltaTime;
            TempEulerFloat = Mathf.Clamp(TempEulerFloat, -30, 30);
            //Camera_Handle.transform.localEulerAngles =Vector3.Lerp(Camera_Handle.transform.localEulerAngles, new Vector3(-TempEulerFloat, 0, 0),2f);

            Main_Camera.transform.eulerAngles = new Vector3(-TempEulerFloat, 0, 0);
            Camera_Handle.transform.localEulerAngles = Main_Camera.transform.eulerAngles;


            model.transform.eulerAngles = tempEuler;

            Main_Camera.transform.LookAt(Camera_Handle.transform);
            Main_Camera.transform.position = Vector3.Lerp(Main_Camera.transform.position, transform.position, 0.1f);

            //PlayerHandle.transform.eulerAngles = tempEuler;
        }
        else
        {
            Vector3 tempEuler = model.transform.eulerAngles;//类似死锁


            Vector3 tempV = targetObject.transform.position - model.transform.position;
            tempV.y = 0;
            //model.transform.forward = tempV;
            PlayerHandle.transform.forward = tempV;
            Main_Camera.transform.LookAt(Camera_Handle.transform);
            Main_Camera.transform.position = Vector3.Lerp(Main_Camera.transform.position, transform.position, 0.1f);
            model.transform.eulerAngles = tempEuler;

        }
    }

    public void LockUnlock()
    {

        Vector3 CapPos = model.transform.position;
        Vector3 Transition = CapPos + new Vector3(0, 1, 0);
        Vector3 BoxCenter = Transition + model.transform.forward * (5f + cap.radius + 0.5f);
        Collider[] cos = Physics.OverlapBox(BoxCenter, new Vector3(1f, 1f, 5f), model.transform.rotation, LayerMask.GetMask("enemy"));
        if (cos.Length == 0)
        {
            targetObject = null;
            Lockdot.enabled = false;
            LockState = false;
        }
        else
        {
            if (cos[0].gameObject == targetObject)
            {
                targetObject = null;
                Lockdot.enabled = false;
                LockState = false;
            }
            else
            {
                foreach (var cosInformation in cos)
                {
                    targetObject = cosInformation.gameObject;
                    Lockdot.enabled = true;
                     LockState = true;
                    break;
                }
            }
        }
    }
}
