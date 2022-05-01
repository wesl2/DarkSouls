using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    Animator Am;
    private void Awake()
    {
        Am = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        Am.SendMessageUpwards("RootMove", (object)Am.deltaPosition);
    }
}
