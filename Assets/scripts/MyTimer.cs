using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{
    public enum ESTATE
    {
        IDLE,
        RUN,
        FINISHED
    }

    public float duration;
    private float elapsed = 0f;
    public ESTATE estate;
    public void Tick( )
    {
        switch (estate)
        {
            case ESTATE.IDLE:
                 break; 
            case ESTATE.RUN:
                {
                    elapsed += Time.deltaTime;
                    
                    if (elapsed >= duration)
                    estate = ESTATE.FINISHED; 


                    break;
                }
            case ESTATE.FINISHED:
                { break; }
            default:
                break;
        }
    }
    public void GO( )
    {
        elapsed = 0f;
        estate = ESTATE.RUN;
    }

 }
