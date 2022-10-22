using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationControl : MonoBehaviour
{
    public Animator myAni;
    public string targetInt;
    // Start is called before the first frame update
    void Start()
    {
        if(myAni == null) { myAni = GetComponent<Animator>();}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeState(string target)
    {
        myAni.Play(target);
    }

    public void activeBool(string boolName)
    {
        myAni.SetBool(boolName, true);
    }

    public void deactiveBool(string boolName)
    {
        myAni.SetBool(boolName, false);
    }

    public void activeTrigger(string triggerName)
    {
        myAni.SetTrigger(triggerName);
    }

    public void SetTargetInt(string i)
    {
        targetInt = i;
    }

    public void ChangeTargetInt(int toChange)
    {
        myAni.SetInteger(targetInt, toChange);
    }
}
