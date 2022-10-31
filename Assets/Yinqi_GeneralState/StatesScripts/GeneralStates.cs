using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GeneralStateBase: MonoBehaviour
{
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;
    public virtual void EnterState(GameManager gm)
    {
        if(enterEvent != null) 
        { 
            enterEvent.Invoke();
        }
    }
    public virtual void Process(GameManager gm)
    {

    }
    //public abstract void FixedUpdate(GameManager gm);
    public virtual void ExitState(GameManager gm)
    {
        if (exitEvent != null)
        {
            exitEvent.Invoke();
        }
    }
}




