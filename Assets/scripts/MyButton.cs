using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton  
{
   public bool isPressing = false;
   public bool  onPressed = false;
   public bool onReleased  = false;

    public bool isExtending = false;
    public bool isDelaying = false;


   public bool currentState = false;
   public bool lastState = false;


    float DurationTime = 1f;
    float DelayTime = 0.56f;


    private MyTimer exTimer = new MyTimer();
    private MyTimer StartTimer = new MyTimer();
    
    
    public void Tick(bool input)
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    exTimer.duration = 3f;
        //    exTimer.GO();
        //}
        exTimer.Tick();
        StartTimer.Tick();
        //Debug.Log(exTimer.estate);
            
        
        currentState = input;

        isPressing = currentState;
        isExtending = false;
        isDelaying = false;

        onPressed = false;
        if(currentState != lastState)
        {
            if(currentState==true)
            {
                onPressed = true;
                startMyTimer(StartTimer, DelayTime);
            }
        }

        onReleased = false;
        if(currentState != lastState)
        {
            if(lastState == true)
            {
                onReleased = true;
                startMyTimer(exTimer, DurationTime);
            }
        }
        lastState = currentState;


        if (exTimer.estate == MyTimer.ESTATE.RUN)
        { isExtending = true; }

        if (StartTimer.estate == MyTimer.ESTATE.RUN)
        { isDelaying = true; }
    }
    void startMyTimer(MyTimer Timer,float duration)
    {
        Timer.duration = duration;
        Timer.GO();
    }
}
