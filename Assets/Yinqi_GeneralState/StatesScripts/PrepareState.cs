using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareState : GeneralStateBase
{
    public override void EnterState(GameManager gm)
    { 
        //battle systems should be disabled
        base.EnterState(gm);
        //Show prepare UI
        gm.prepareUI.SetActive(true);
    }
    public override void Process(GameManager gm)
    {
        base.Process(gm);
        gm.timer["prepare"] += Time.deltaTime;
        gm.prepareTimer.GetComponent<TMPro.TMP_Text>().text = gm.timer["prepare"].ToString();
    }
    /*public abstract void FixedUpdate(GameManager gm)
    { }*/
    public override void ExitState(GameManager gm)
    {
        base.ExitState(gm); 
        //clearUp
        gm.timer["prepare"] = 0f;
        //Deactivate prepare state
        gm.prepareUI.SetActive(false);
    }
}
