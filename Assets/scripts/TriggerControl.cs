using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    Animator Am;
     void Awake()
    {
        Am = GetComponent<Animator>();
    }
    public void resetTrigger(string triggerName)
    {
        Am.ResetTrigger(triggerName);
    }
}
